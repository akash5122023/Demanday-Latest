
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class SulekhaDetailsGrid extends GridBase<SulekhaDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.SulekhaDetails'; }
        protected getDialogType() { return SulekhaDetailsDialog; }
        protected getIdProperty() { return SulekhaDetailsRow.idProperty; }
        protected getInsertPermission() { return SulekhaDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return SulekhaDetailsRow.localTextPrefix; }
        protected getService() { return SulekhaDetailsService.baseUrl; }

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
                    
                    var action = new ThirdParty.SulekhaBulkDialog();
                    action.element.on('dialogclose', () => {
                        action.MIds = this.rowSelection.getSelectedKeys();
                      
                    });
                    action.dialogOpen();
                },
                //// dialog.dialogOpen();
                //action.dialogOpen();
                separator: true

            });

            return buttons;
        }
    }
}