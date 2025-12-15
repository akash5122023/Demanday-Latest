
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class InstamojoDetailsGrid extends GridBase<InstamojoDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.InstamojoDetails'; }
        protected getDialogType() { return InstamojoDetailsDialog; }
        protected getIdProperty() { return InstamojoDetailsRow.idProperty; }
        protected getInsertPermission() { return InstamojoDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return InstamojoDetailsRow.localTextPrefix; }
        protected getService() { return InstamojoDetailsService.baseUrl; }

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
                    InstamojoDetailsService.Sync({},
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