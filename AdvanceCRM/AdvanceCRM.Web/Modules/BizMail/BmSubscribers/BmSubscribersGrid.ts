
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmSubscribersGrid extends GridBase<BmSubscribersRow, any> {
        protected getColumnsKey() { return 'BizMail.BmSubscribers'; }
        protected getDialogType() { return BmSubscribersDialog; }
        protected getIdProperty() { return BmSubscribersRow.idProperty; }
        protected getInsertPermission() { return BmSubscribersRow.insertPermission; }
        protected getLocalTextPrefix() { return BmSubscribersRow.localTextPrefix; }
        protected getService() { return BmSubscribersService.baseUrl; }

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
                    BmSubscribersService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            return buttons;
        }
    }
}