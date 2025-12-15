
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class InteraktUserGrid extends GridBase<InteraktUserRow, any> {
        protected getColumnsKey() { return 'ThirdParty.InteraktUser'; }
        protected getDialogType() { return InteraktUserDialog; }
        protected getIdProperty() { return InteraktUserRow.idProperty; }
        protected getInsertPermission() { return InteraktUserRow.insertPermission; }
        protected getLocalTextPrefix() { return InteraktUserRow.localTextPrefix; }
        protected getService() { return InteraktUserService.baseUrl; }

       
            constructor(container: JQuery) {
                super(container);

                //InteraktUserService.Sync({},
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
                      InteraktUserService.Sync({},
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
                        
                        var action = new ThirdParty.InteraktUserBulkDialog();
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