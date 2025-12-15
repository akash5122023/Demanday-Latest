
namespace AdvanceCRM.ThirdParty {
    import fld = AdvanceCRM.ThirdParty.WatiContactsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class WatiContactsGrid extends GridBase<WatiContactsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.WatiContacts'; }
        protected getDialogType() { return WatiContactsDialog; }
        protected getIdProperty() { return WatiContactsRow.idProperty; }
        protected getInsertPermission() { return WatiContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return WatiContactsRow.localTextPrefix; }
        protected getService() { return WatiContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            WatiContactsService.Sync({},
                response => { }
            );
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    WatiContactsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: " text-blue",
                onClick: () => {

                    var action = new ThirdParty.WatiContactsBulkDialog();
                    action.element.on('dialogclose', () => {
                        action.MIds = this.rowSelection.getSelectedKeys();
                    });
                    action.dialogOpen();
                },
                separator: true
            });

            return buttons;
        }
    }
}