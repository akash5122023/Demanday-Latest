
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class WcOrderDetailsGrid extends GridBase<WcOrderDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.WcOrderDetails'; }
        protected getDialogType() { return WcOrderDetailsDialog; }
        protected getIdProperty() { return WcOrderDetailsRow.idProperty; }
        protected getInsertPermission() { return WcOrderDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return WcOrderDetailsRow.localTextPrefix; }
        protected getService() { return WcOrderDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    WcOrderDetailsService.Sync({},
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

                    var action = new ThirdParty.WcOrderBulkDialog();
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