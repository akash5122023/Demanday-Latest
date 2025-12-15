
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class TradeIndiaDetailsGrid extends GridBase<TradeIndiaDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.TradeIndiaDetails'; }
        protected getDialogType() { return TradeIndiaDetailsDialog; }
        protected getIdProperty() { return TradeIndiaDetailsRow.idProperty; }
        protected getInsertPermission() { return TradeIndiaDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return TradeIndiaDetailsRow.localTextPrefix; }
        protected getService() { return TradeIndiaDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            //TradeIndiaDetailsService.Sync({},
            //    response => { }
            //);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    TradeIndiaDetailsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            //buttons.push({
            //    title: 'Bulk Move',
            //    hint: 'Bulk Move to Enquiry',
            //    icon: " text-blue",
            //    onClick: () => {

            //        var action = new ThirdParty.TradeIndiaBulkDialog();
            //        action.element.on('dialogclose', () => {
            //            action.MIds = this.rowSelection.getSelectedKeys();
            //        });
            //        action.dialogOpen();
            //    },
            //    separator: true
            //});
            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: "text-blue",
                onClick: () => {
                    const selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected rows

                    if (!selectedKeys || selectedKeys.length === 0) {
                        Q.notifyError("Please select at least one row!");
                        return;
                    }

                    // Create dialog instance
                    const action = new ThirdParty.TradeIndiaBulkDialog();

                    // Assign selected rows to the dialog's MIds
                    action.MIds = selectedKeys;

                    // Open the dialog
                    action.dialogOpen();
                },
                separator: true
            });

            return buttons;
        }
    }
}