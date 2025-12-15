
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmListGrid extends GridBase<BmListRow, any> {
        protected getColumnsKey() { return 'BizMail.BmList'; }
        protected getDialogType() { return BmListDialog; }
        protected getIdProperty() { return BmListRow.idProperty; }
        protected getInsertPermission() { return BmListRow.insertPermission; }
        protected getLocalTextPrefix() { return BmListRow.localTextPrefix; }
        protected getService() { return BmListService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            // buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    BmListService.Sync({},
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