
namespace AdvanceCRM.Services {

    import fld = CMSRow.Fields;
    export class TeleCallingGrid extends GridBase<TeleCallingRow, any> {
        protected getColumnsKey() { return 'Services.TeleCalling'; }
        protected getDialogType() { return TeleCallingDialog; }
        protected getIdProperty() { return TeleCallingRow.idProperty; }
        protected getInsertPermission() { return TeleCallingRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleCallingRow.localTextPrefix; }
        protected getService() { return TeleCallingService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            var q = Q.parseQueryString();
            if (q["Open"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
                var user = Q.tryFirst(Administration.UserRow.getLookup().items, x => x.Username == Q.Authorization.userDefinition.Username);
                this.findQuickFilter(Administration.UserEditor, fld.AssignedTo).value = Q.toId(user.UserId);
            }
            else if (q["OpenTeam"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
            }

            this.element.find('.quick-filters-bar').toggle(false)
        }






        getButtons() {
            var buttons = super.getButtons();
            //var filterButton = buttons.pop();

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

           // buttons.push(filterButton);

            return buttons;
        }










    }
}