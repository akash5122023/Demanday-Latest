
namespace AdvanceCRM.Enquiry {

    import fld = Enquiry.EnquiryRow.Fields;
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.filterable()
    export class EnquiryGrid extends GridBase<EnquiryRow, any> {
        protected getColumnsKey() { return 'Enquiry.Enquiry'; }
        protected getDialogType() { return EnquiryDialog; }
        protected getIdProperty() { return EnquiryRow.idProperty; }
        protected getInsertPermission() { return EnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryRow.localTextPrefix; }
        protected getService() { return EnquiryService.baseUrl; }

        private pendingChanges: Q.Dictionary<any> = {};

        constructor(container: JQuery) {
            super(container);

           // this.slickContainer.on('change', '.edit:input', (e) => this.inputsChange(e));

            //new Serenity.HeaderFiltersMixin({
            //    grid: this
            //});

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

        //Quick filter for products
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
                    (this.view.params as EnquiryListRequest).ProductsId = Q.toId(w.value);
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
                    (this.view.params as EnquiryListRequest).AreaId = Q.toId(w.value);
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
                    (this.view.params as EnquiryListRequest).DivisionId = Q.toId(w.value);
                }
            });

            return filters;
        }

        //Sum at bottom
        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('Total')
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


      

        protected getItemCssClass(item: Enquiry.EnquiryRow, index: number): string {
            let klass: string = "";

            if ((item.Type == 1) && (item.Status == 1)) {
                klass += " hot-enquiry";
            }
            else if ((item.Type == 2) && (item.Status == 1))
                klass += " warm-enquiry";
            else if ((item.Type == 3) && (item.Status == 1))
                klass += " cold-enquiry";

            return Q.trimToNull(klass);
        }

        ///New

       

        protected getColumns():Slick.Column[] {
            var columns = super.getColumns();

            if (Authorization.hasPermission("IVR:Click To Call")) {
                columns.push({
                    field: "CallRow",
                    name: "Call",
                    format: ctx => {
                        return `<a class='inline-action call-row s-CallButton' title='IVR Call'>
                        <i class='fa fa-phone text-blue'></i>
                    </a>`;
                    },
                    width: 50,
                    minWidth: 50,
                    maxWidth: 50
                });
            }

            // adding a specific css class to UnitPrice column, 
            // to be able to format cell with a different background
            Q.first(columns, x => x.field == fld.Type).cssClass += " col-unit-Enqcolor";


            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ContactPersonProject"), 1);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].WinPercentageInEnquiry != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "WinPercentage"), 1);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ExpectedClosingDateInEnquiry != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ExpectedClosingDate"), 1);
            }
           
            return columns;
        }

        protected onClick(e: JQueryEventObject, row: number, cell: number): void {
            super.onClick(e, row, cell);

            let target = $(e.target);

            if (target.closest(".call-row").length) {
                let item = this.itemAt(row);
                if (!item) return;

                let phn = item.ContactsPhone;
                if (item.ContactsContactType !== 1 && item.ContactPersonId) {
                    phn = item.ContactPersonPhone;
                }

                if (phn) {
                    var dialog = new ThirdParty.IVRCallDialog({
                        CustomerNumber: phn,
                        ServiceURL: this.getService()
                    });

                    dialog.dialogOpen();
                } else {
                    Q.notifyError("Contact Invalid!!");
                }
            }
        }




        getButtons() {
            var buttons = super.getButtons();
            var filterButton = buttons.pop();

            buttons.push({
                title: 'Import',
                cssClass: 'export-xlsx-button',
                hint: "Import Enquiries",
                onClick: () => {
                    // open import dialog, let it handle rest
                    var dialog = new EnquiryExcelImportDialog();
                    dialog.element.on('dialogclose', () => {
                        this.refresh();
                        dialog = null;
                    });
                    dialog.dialogOpen();
                },
                separator: true
            });

            if (Authorization.hasPermission("MailChimp:Inbox")) {
                buttons.push({
                    title: 'MailChimp',
                    //cssClass: 'send-button',
                    icon: 'fa-bullhorn text-green',
                    hint: 'Add selected Contacts to MailChimp',

                    onClick: () => {

                        Q.confirm('Do you want to add these contacts to default Enquiry List on MailChimp?\nPress yes to continue or press No to choose another list', () => EnquiryService.AddToMailChimp({
                            MailChimpIds: this.rowSelection.getSelectedKeys(),
                            ListName: "Enquiry List"
                        },
                            response => {
                                if (response.MailChimpReturnResponse == "Success") {
                                    this.rowSelection.resetCheckedAndRefresh();
                                    Q.information('Selected contact added successfully to MailChimp Enquriy list', () => { Q.resolveUrl('#'); });
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
                                    dlg.MModule = "Enquiry";
                                    dlg.dialogOpen();
                                }
                            }
                        );

                    },
                    separator: true
                });
            }
            buttons.push({
                title: 'QuickMail',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                hint: 'Quick Mail Sender',
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
                            email: row.ContactsEmail
                        };
                    }).filter(c => c.email); // Ensure valid contacts

                    if (contacts.length === 0) {
                        Q.alert("No valid contacts found.");
                        return;
                    }

                    var dialog = new Common.QuickMailDialog({ Contacts: contacts });

                    dialog.element.on('dialogclose', () => {
                        dialog = null;
                    });

                    dialog.dialogOpen();
                }
                ,
                separator: true
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
            //buttons.push({
            //    title: 'BizWhatsApp',
            //    cssClass: 'send-button',
            //    icon: 'fa-brands fa-whatsapp',
            //    hint: 'Bizplus Whatsapp Sender',
            //    onClick: () => {
            //        var selectedRows = this.rowSelection.getSelectedKeys(); // Get selected row IDs
            //        if (!selectedRows || selectedRows.length === 0) {
            //            Q.notifyError("Please select at least one contact.");
            //            return;
            //        }

            //        var contacts = selectedRows.map(id => {
            //            var row = this.view.getItemById(id);

            //            console.log("Selected ID:", id);
            //            console.log("Retrieved Row:", row);

            //            if (!row) {
            //                console.warn(`Row not found for ID: ${id}`);
            //                return null;
            //            }

            //            return {
            //                id: id,
            //                number: row.ContactsWhatsapp || row.ContactsPhone || "No Number",
            //                name: row.ContactsName || "No Name"
            //            };
            //        }).filter(c => c !== null);


            //        var index = 0; // Start with the first contact

            //        function openDialog() {
            //            if (index >= contacts.length) {
            //                return; // Stop if all contacts are processed
            //            }

            //            var contact = contacts[index];
            //            index++; // Move to the next contact for the next dialog

            //            var dialog = new Common.IntractWaDialog({
            //                Number: contact.number,
            //                Name: contact.name
            //            });

            //            dialog.element.on('dialogclose', () => {
            //                dialog = null;
            //                openDialog(); // Open the next dialog after closing the previous one
            //            });

            //            dialog.dialogOpen();
            //        }

            //        openDialog(); // Start sending messages one by one
            //    },
            //    separator: true
            //});

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

            /////////////////

            //if (Authorization.hasPermission("SMS:BulkMail")) {
            //    buttons.push(
            //        {
            //            title: 'Bulk Mail',
            //            icon: 'fa-comments-o text-green',
            //            onClick: () => {
            //                var dialog = new Common.BulkMailDialog({
            //                    Ids: this.rowSelection.getSelectedKeys(),
            //                    ServiceURL: this.getService()
                                
            //                });

            //                dialog.element.on('dialogclose', () => {
            //                    this.rowSelection.resetCheckedAndRefresh();
            //                    dialog = null;
            //                });

            //                dialog.dialogOpen();
            //            }
            //        }
            //    );
            //}

            //Buttons for groupping
            buttons.push(
                {
                    title: 'Status',
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
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
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
                    onClick: () => {
                        this.view.setGrouping([])
                    }
                }
            );

            buttons.push(filterButton);

            return buttons;
        }
    }
}