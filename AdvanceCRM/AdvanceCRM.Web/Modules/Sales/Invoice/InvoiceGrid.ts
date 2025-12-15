
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceGrid extends GridBase<InvoiceRow, any> {
        protected getColumnsKey() { return 'Sales.Invoice'; }
        protected getDialogType() { return InvoiceDialog; }
        protected getIdProperty() { return InvoiceRow.idProperty; }
        protected getInsertPermission() { return InvoiceRow.insertPermission; }
        protected getLocalTextPrefix() { return InvoiceRow.localTextPrefix; }
        protected getService() { return InvoiceService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('GrandTotal')
                ]
            });

            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            return grid;
        }

        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }

        getButtons() {
            var buttons = super.getButtons();

            var filterButton = buttons.pop();

            if (Authorization.hasPermission("MailChimp:Inbox")) {
                buttons.push({
                    title: 'MailChimp',
                    icon: 'fa-bullhorn text-green',
                    hint: 'Send thankyou SMS to customer',

                    onClick: () => {

                        Q.confirm('Do you want to add these contacts to default Proforma List on MailChimp?\nPress yes to continue or press No to choose another list', () => InvoiceService.AddToMailChimp({
                            MailChimpIds: this.rowSelection.getSelectedKeys(),
                            ListName: "Proforma List"
                        },
                            response => {
                                if (response.MailChimpReturnResponse == "Success") {
                                    this.rowSelection.resetCheckedAndRefresh();
                                    Q.information('Selected contact added successfully to MailChimp Proforma list', () => { Q.resolveUrl('#'); });
                                }
                                else {
                                    if (response.MailChimpReturnResponse == "In selected list no valid email Id where found") {
                                        Q.alert(response.MailChimpReturnResponse);
                                    }
                                    else {
                                        Q.alert('Error occurred while adding selected contacts to MailChimp list\n' + response.MailChimpReturnResponse);
                                    }
                                }
                            },
                            { async: true }
                        ),
                            {
                                onNo: () => {
                                    var dlg = new MailChimpList.MailChimpListDialog();
                                    dlg.MIds = this.rowSelection.getSelectedKeys();
                                    dlg.MModule = "Invoice";
                                    dlg.dialogOpen();
                                }
                            }
                        );

                    },
                    separator: true
                });
            }
             
           
                buttons.push({
                    title: 'Create Invoice',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move selected Invoices to Sales Invoices',
                    onClick: () => {
                        const selectedKeys = this.rowSelection.getSelectedKeys();
                        console.log("Selected Keys:", selectedKeys);

                        if (selectedKeys.length === 0) {
                            Q.alert('Please select at least one record.');
                            return;
                        }

                        this.toolbar.findButton("Create Invoice").prop("disabled", true);

                        let createdCount = 0;
                        let remaining = selectedKeys.length;

                        const delay = 500; // Add delay to prevent UI overlap
                        selectedKeys.forEach((id, index) => {
                            window.setTimeout(() => {
                                InvoiceService.MoveToInvoice({
                                    Id: Q.toId(id),
                                    MailType: "Sales"
                                }, response => {
                                    console.log("Sales Response:", response);
                                    if (response.Id > 0) {
                                        createdCount++;
                                        let dialog = new Sales.SalesDialog();
                                        dialog.loadByIdAndOpenDialog(response.Id);

                                        //new Sales.SalesDialog().loadByIdAndOpenDialog(response.Id);
                                    } else {
                                        Q.notifyError(response.Status || "Failed to convert Invoice ID: " + id);
                                    }

                                    remaining--;
                                    if (remaining === 0) {
                                        this.toolbar.findButton("Create Invoice").prop("disabled", false);
                                        Q.notifySuccess(`${createdCount} Sales Invoice(s) created successfully.`);
                                        this.rowSelection.resetCheckedAndRefresh();
                                    }
                                });
                            }, index * delay);
                        });
                    }
                });
            

            


            if (Authorization.hasPermission("SMS:BulkSMS")) {
                buttons.push(
                    {
                        title: 'SMS',
                        icon: 'fa-comments-o text-green',
                        onClick: () => {
                            var dialog = new Common.BulkSMSDialog({
                                Ids: this.rowSelection.getSelectedKeys(),
                                ServiceURL: this.getService()
                            });

                            dialog.element.on('dialogclose', () => {
                                this.rowSelection.resetCheckedAndRefresh();
                                dialog = null;
                            });

                            dialog.dialogOpen();
                        }
                    }
                );
            }

            buttons.push({
                title: 'BizWhatsApp',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                hint: 'Bizplus Whatsapp Sender',
                onClick: () => {
                    var selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected row IDs
                    if (selectedKeys.length === 0) {
                        Q.alert("Please select at least one contact.");
                        return;
                    }

                    var contacts = selectedKeys.map(id => {
                        var row = this.view.getItemById(id);
                        return {
                            id: id,
                            number: row.ContactsWhatsapp || row.ContactsPhone,
                            name: row.ContactsName
                        };
                    }).filter(c => c.number); // Ensure valid contacts

                    if (contacts.length === 0) {
                        Q.alert("No valid contacts found.");
                        return;
                    }

                    var dialog = new Common.IntractWaDialog({ Contacts: contacts });

                    dialog.element.on('dialogclose', () => {
                        dialog = null;
                    });

                    dialog.dialogOpen();
                }
                ,
                separator: true
            });

            if (Authorization.hasPermission("SMS:BulkMail")) {
                buttons.push(
                    {
                        title: 'Bulk Mail',
                        icon: 'fa-comments-o text-green',
                        hint: 'BulkMail',

                        onClick: () => {
                            var dialog = new Common.QuickBizEmailDialog({
                                Ids: this.rowSelection.getSelectedKeys(),
                                ServiceURL: this.getService()
                            });

                            dialog.element.on('dialogclose', () => {
                                this.rowSelection.resetCheckedAndRefresh();
                                dialog = null;
                            });

                            dialog.dialogOpen();
                        },
                        separator: true
                    });
            }

            //Buttons for groupping
            buttons.push(
                {
                    title: 'Status',
                    cssClass: 'expand-all-button',
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
                        this.view.setGrouping(
                            [{
                                formatter: x => Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get('Masters.Status'), Q.toId(x.value)),
                                getter: 'Status'
                            }])
                    },
                    separator: true
                }
            );

            buttons.push(
                {
                    title: 'Type',
                    cssClass: 'expand-all-button',
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
                        this.view.setGrouping(
                            [{
                                formatter: x => Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get('Masters.InvoiceType'), Q.toId(x.value)),
                                getter: 'Type'
                            }])
                    }
                }
            );

            buttons.push(
                {
                    title: 'Grouping',
                    cssClass: 'delete-button',
                    onClick: () => this.view.setGrouping([])
                }
            );

            buttons.push(filterButton);

            return buttons;
        }

        //Adding Print Preview for Invoice
        protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            let inlineActionsColumnWidth = 22;
            let inlineActionsColumnContent = `<a class="inline-actions print-row" title="${q.text('Controls.Print', 'Print')}" style="padding-left: 5px;"><i class="print-row fa fa-print"></i></a>`;

            columns.splice(1, 0, {
                field: 'inline-actions',
                name: '',
                cssClass: 'inline-actions-column',
                width: inlineActionsColumnWidth,
                minWidth: inlineActionsColumnWidth,
                maxWidth: inlineActionsColumnWidth,
                format: ctx => inlineActionsColumnContent
            });

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ContactPersonProject"), 1);
            }

            return columns;
        }

        //Adding Invoice preview button
        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Invoice.PrintInvoice',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }

    }
}