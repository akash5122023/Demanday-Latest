
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesGrid extends GridBase<SalesRow, any> {
        protected getColumnsKey() { return 'Sales.Sales'; }
        protected getDialogType() { return SalesDialog; }
        protected getIdProperty() { return SalesRow.idProperty; }
        protected getInsertPermission() { return SalesRow.insertPermission; }
        protected getLocalTextPrefix() { return SalesRow.localTextPrefix; }
        protected getService() { return SalesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
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
                    (this.view.params as SalesListRequest).ProductsId = Q.toId(w.value);
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
                    (this.view.params as SalesListRequest).AreaId = Q.toId(w.value);
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
                    (this.view.params as SalesListRequest).DivisionId = Q.toId(w.value);
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

                        Q.confirm('Do you want to add these contacts to default Sale List on MailChimp?\nPress yes to continue or press No to choose another list', () => SalesService.AddToMailChimp({
                            MailChimpIds: this.rowSelection.getSelectedKeys(),
                            ListName: "Sale List"
                        },
                            response => {
                                if (response.MailChimpReturnResponse == "Success") {
                                    this.rowSelection.resetCheckedAndRefresh();
                                    Q.information('Selected contact added successfully to MailChimp Sale list', () => { Q.resolveUrl('#'); });
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
                                    dlg.MModule = "Sales";
                                    dlg.dialogOpen();
                                }
                            }
                        );

                    },
                    separator: true
                });
            }

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


            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Sales.PrintSale', // Replace with your actual report key
                getParams: () => {
                    // Get selected row(s)
                    let selectedKeys = this.rowSelection.getSelectedKeys();

                    if (selectedKeys.length === 0) {
                        Q.alert("Please select a record to preview.");
                        return {};
                    }


                    //var contacts = selectedKeys.map(id => {
                    //    var row = this.view.getItemById(id);
                    //    return {
                    //        id: id,
                    //        name: row?.ContactsName||""
                    //    };
                    //});
                    //if (contacts.length === 0 || contacts.some(x => !x.name)) {
                    //    Q.alert("No valid contacts found.");
                    //    return false;
                    //}

                    let contacts: { id: string, name: string }[] = [];

                    for (let id of selectedKeys) {
                        let row = this.view.getItemById(id);
                        if (!row || !row.ContactsName) {
                            Q.alert("Some selected rows do not have contact information.");
                            return false;
                        }
                        contacts.push({ id: id, name: row.ContactsName });
                    }
                    let firstName = contacts[0].name.trim().toLowerCase();
                    let allSame = contacts.every(c => c.name.trim().toLowerCase() === firstName);

                    if (!allSame) {
                        Q.alert("Please select records with the same Contact Name.");
                        return false;
                    }


                    return { Ids: selectedKeys };

                    // Pass array of IDs instead of one
                   
                    //let id = selectedKeys[0]; // Only one item for preview
                    //return { Id: id };
                },
                separator: true
            }));

            //buttons.push({
            //    title: 'Preview',
            //    cssClass: 'print-preview-button',
            //    icon: 'fa fa-print',
            //    hint: 'Print selected invoice(s)',
            //    onClick: () => {
            //        let selectedIds = this.rowSelection.getSelectedKeys();

            //        if (selectedIds.length === 0) {
            //            Q.alert("Please select at least one invoice to print.");
            //            return;
            //        }
                
            //        _Ext.ReportHelper.execute({ reportKey: 'Reports.SalesReportIndex', params: { Request: { Ids: selectedIds } }, extension: 'pdf' });

                   
            //    },
            //    separator: true
                
            //});
           

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


        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Sales.PrintSale',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }
    }
}