
namespace AdvanceCRM.Quotation {

    import fld = Quotation.QuotationRow.Fields;
    @Serenity.Decorators.registerClass()
    export class QuotationGrid extends GridBase<QuotationRow, any> {
        protected getColumnsKey() { return 'Quotation.Quotation'; }
        protected getDialogType() { return QuotationDialog; }
        protected getIdProperty() { return QuotationRow.idProperty; }
        protected getInsertPermission() { return QuotationRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationRow.localTextPrefix; }
        protected getService() { return QuotationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            var q = Q.parseQueryString();
            if (q["Open"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
                var user = Q.tryFirst(Administration.UserRow.getLookup().items, x => x.Username == Q.Authorization.userDefinition.Username);
                this.findQuickFilter(Administration.UserEditor, fld.AssignedId).value = Q.toId(user.UserId);
            }
            else if (q["OpenTeam"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
            }
        }
        
        protected getQuickFilters() {
            var filters = super.getQuickFilters();

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: Products.ProductsRow.lookupKey
                },
                field: 'Id',
                title: 'Product',
                handler: w => {
                    (this.view.params as QuotationListRequest).ProductsId = Q.toId(w.value);
                }
            });

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: AdvanceCRM.Masters.AreaRow.lookupKey
                },
                field: 'Id',
                title: 'Area',
                handler: w => {
                    (this.view.params as QuotationListRequest).AreaId = Q.toId(w.value);
                }
            });

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: AdvanceCRM.Masters.ProductsDivisionRow.lookupKey
                },
                field: 'Id',
                title: 'Division',
                handler: w => {
                    (this.view.params as QuotationListRequest).DivisionId = Q.toId(w.value);
                }
            });

            return filters;
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('GrandTotal')
                ]
            });

            // for goruping need to register this plugin for grouping or you'll have errors
            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            return grid;
        }

        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }

        //Conditional formatiing
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

            // adding a specific css class to UnitPrice column, 
            // to be able to format cell with a different background
            Q.first(columns, x => x.field == fld.Type).cssClass += " col-unit-Quotcolor";
                                
            const companySettings = Administration.CompanyDetailsHelper.getCurrent();

            if (companySettings?.ProjectWithContacts != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ContactPersonProject"), 1);
            }

            if (companySettings?.WinPercentageInQuotation != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "WinPercentage"), 1);
            }

            if (companySettings?.ExpectedClosingDateInQuotation != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ExpectedClosingDate"), 1);
            }

            return columns;
        }

        //Highlighting Hot,Warm, Cold

        protected getItemCssClass(item: Quotation.QuotationRow, index: number): string {
            let klass: string = "";

            if ((item.Type == 1) && (item.Status == 1)) {
                klass += " hot-quotation";
            }
            else if ((item.Type == 2) && (item.Status == 1))
                klass += " warm-quotation";
            else if ((item.Type == 3) && (item.Status == 1))
                klass += " cold-quotation";

            return Q.trimToNull(klass);
        }

      

        getButtons() {
            var buttons = super.getButtons();

            var filterButton = buttons.pop();
            if (Authorization.hasPermission("MailChimp:Inbox")) {
                buttons.push({
                    title: 'MailChimp',
                    //cssClass: 'send-button',
                    icon: 'fa-bullhorn text-green',
                    hint: 'Send thankyou SMS to customer',

                    onClick: () => {

                        Q.confirm('Do you want to add these contacts to default Quotation List on MailChimp?\nPress yes to continue or press No to choose another list', () => QuotationService.AddToMailChimp({
                            MailChimpIds: this.rowSelection.getSelectedKeys(),
                            ListName: "Quotation List"
                        },
                            response => {
                                if (response.MailChimpReturnResponse == "Success") {
                                    this.rowSelection.resetCheckedAndRefresh();
                                    Q.information('Selected contact added successfully to MailChimp Quotation list', () => { Q.resolveUrl('#'); });
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
                                    dlg.MModule = "Quotation";
                                    dlg.dialogOpen();
                                }
                            }
                        );

                    },
                    separator: true
                });
            }

            //buttons.push({
            //    title: 'QuickMail',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Quick Mail Sender',
            //    onClick: () => {
            //        var selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected row IDs
            //        if (selectedKeys.length === 0) {
            //            Q.alert("Please select at least one contact.");
            //            return;
            //        }

            //        var contacts = selectedKeys.map(id => {
            //            var row = this.view.getItemById(id);
            //            return {
            //                id: id,
            //                email: row.ContactsEmail
            //            };
            //        }).filter(c => c.email); // Ensure valid contacts

            //        if (contacts.length === 0) {
            //            Q.alert("No valid contacts found.");
            //            return;
            //        }

            //        var dialog = new Common.QuickMailDialog({ Contacts: contacts });

            //        dialog.element.on('dialogclose', () => {
            //            dialog = null;
            //        });

            //        dialog.dialogOpen();
            //    }
            //    ,
            //    separator: true
            //});

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
            if (Authorization.hasPermission("Quotation:Move to Invoice")) {
                buttons.push({
                    title: 'Create Invoice',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move selected Quotations to Sales Invoices',
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
                                QuotationService.MoveToInvoice({
                                    Id: Q.toId(id),
                                    MailType: "Sales"
                                }, response => {
                                    console.log("Sales Response:", response);
                                    if (response.Id > 0) {
                                        createdCount++;
                                        new Sales.SalesDialog().loadByIdAndOpenDialog(response.Id);
                                    } else {
                                        Q.notifyError(response.Status || "Failed to convert Quotation ID: " + id);
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
            }



            if (Authorization.hasPermission("Quotation:Move to Proforma")) {
                // Button for moving to Proforma Invoice
                buttons.push({
                    title: 'Create Proforma',
                    icon: 'fa fa-share-square text-blue',
                    hint: 'Move selected Quotations to Proforma Invoices',
                    onClick: () => {
                        const selectedKeys = this.rowSelection.getSelectedKeys();

                        if (selectedKeys.length === 0) {
                            Q.alert('Please select at least one record.');
                            return;
                        }

                        // Disable the button during processing
                        this.toolbar.findButton("Create Proforma").prop("disabled", true);

                        let createdCount = 0;
                        let remaining = selectedKeys.length;

                        const delay = 500; // Add delay to prevent UI overlap
                        selectedKeys.forEach((id, index) => {
                            window.setTimeout(() => {
                                QuotationService.MoveToInvoice({
                                    Id: Q.toId(id),
                                    MailType: "Invoice"
                                }, response => {
                                    console.log("Invoice Response:", response);
                                    if (response.Id > 0) {
                                        createdCount++;
                                        new Sales.InvoiceDialog().loadByIdAndOpenDialog(response.Id);
                                    } else {
                                        Q.notifyError(response.Status || "Failed to convert Quotation ID: " + id);
                                    }

                                    remaining--;
                                    if (remaining === 0) {
                                        this.toolbar.findButton("Create Performa").prop("disabled", false);
                                        Q.notifySuccess(`${createdCount} Performa Invoice(s) created successfully.`);
                                        this.rowSelection.resetCheckedAndRefresh();
                                    }
                                });
                            }, index * delay);
                        });
                    }
                });
            }

            //Buttons for groupping
            buttons.push(
                {
                    title: 'Status',
                    cssClass: 'expand-all-button',
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
                        //Q.information('Grouping:\n1=Open, 2=Closed, 3=Pending', () => { Q.resolveUrl('#'); });
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
                        //Q.information('Grouping:\n1=Hot, 2=Warm, 3=Cold\nUndefined=Type not selected', () => { Q.resolveUrl('#'); });
                        this.view.setGrouping(
                            [{
                                formatter: x => Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get('Masters.EnquiryType'), Q.toId(x.value)),
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

        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Quotation.PrintQuotation',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }
        
    }
}