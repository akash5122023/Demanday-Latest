
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class VisitGrid extends GridBase<VisitRow, any> {
        protected getColumnsKey() { return 'ThirdParty.Visit'; }
        protected getDialogType() { return VisitDialog; }
        protected getIdProperty() { return VisitRow.idProperty; }
        protected getInsertPermission() { return VisitRow.insertPermission; }
        protected getLocalTextPrefix() { return VisitRow.localTextPrefix; }
        protected getService() { return VisitService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();            

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: " text-blue",
                onClick: () => {

                    var action = new ThirdParty.VisitBulkDialog();
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