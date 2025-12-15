
namespace AdvanceCRM.ThirdParty {
  //  import fld = AdvanceCRM.ThirdParty.WoocommerceDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class WoocommerceDetailsGrid extends GridBase<WoocommerceDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.WoocommerceDetails'; }
        protected getDialogType() { return WoocommerceDetailsDialog; }
        protected getIdProperty() { return WoocommerceDetailsRow.idProperty; }
        protected getInsertPermission() { return WoocommerceDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return WoocommerceDetailsRow.localTextPrefix; }
        protected getService() { return WoocommerceDetailsService.baseUrl; }

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
                    WoocommerceDetailsService.Sync({},
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

                    var action = new ThirdParty.WoocommerceBulkDialog();
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