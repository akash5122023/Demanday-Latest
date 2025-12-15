


namespace AdvanceCRM.Contacts {

    @Serenity.Decorators.registerClass()
    export class ContactsGrid extends GridBase<ContactsRow, any> {
        protected getColumnsKey() { return 'Contacts.Contacts'; }
        protected getDialogType() { return ContactsDialog; }
        protected getIdProperty() { return ContactsRow.idProperty; }
        protected getInsertPermission() { return ContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return ContactsRow.localTextPrefix; }
        protected getService() { return ContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        //protected getColumns() {
        //    var columns = super.getColumns();
        //        columns.splice(2, 0, {
        //            field: 'New Enquiry',
        //            name: '',
        //            format: ctx => `<a class="inline-action new-order text-purple" title="New Enquiry">
        //            <i class="fa fa-cart-plus"></i></a>`,
        //            width: 24,
        //            minWidth: 24,
        //            maxWidth: 24
        //        });

        //    return columns;

        //}

        //Quick filter for products
        protected getQuickFilters() {
            var filters = super.getQuickFilters();

            filters.push({
                type: Serenity.EnumEditor,
                options: {
                    enumKey: "Masters.ContactsStage"
                },
                field: 'Id',
                title: 'Stage',
                handler: w => {
                    (this.view.params as ContactsListRequest).Stage = Q.toId(w.value);
                }
            });

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Contacts.SubContactPhoneLookup"
                },
                field: 'Id',
                title: 'SubContact',
                handler: w => {
                    (this.view.params as ContactsListRequest).SubContactsId = Q.toId(w.value);
                }
            });

            return filters;
        }

        getButtons() {
            var buttons = super.getButtons();
            var filterButton = buttons.pop();

            buttons.push({
                title: 'Import',
                cssClass: 'export-xlsx-button',
                onClick: () => {
                    // open import dialog, let it handle rest
                    var dialog = new Common.ExcelImportDialog(this.getService());
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
                    hint: 'Send thankyou SMS to customer',

                    onClick: () => {

                        Q.confirm('Do you want to add these contacts to default Contacts List on MailChimp?\nPress yes to continue or press No to choose another list', () => ContactsService.AddToMailChimp({
                            MailChimpIds: this.rowSelection.getSelectedKeys(),
                            ListName: "Contacts List"
                        },
                            response => {
                                if (response.MailChimpReturnResponse == "Success") {
                                    this.rowSelection.resetCheckedAndRefresh();
                                    Q.information('Selected contact added successfully to MailChimp Contacts list', () => { Q.resolveUrl('#'); });
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
                                    dlg.MModule = "Contacts";
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

            buttons.push(filterButton);

            return buttons;
        }
    }
}