
namespace AdvanceCRM.ThirdParty {
    import fld = AdvanceCRM.ThirdParty.RpPaymentDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class RpPaymentDetailsGrid extends GridBase<RpPaymentDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.RpPaymentDetails'; }
        protected getDialogType() { return RpPaymentDetailsDialog; }
        protected getIdProperty() { return RpPaymentDetailsRow.idProperty; }
        protected getInsertPermission() { return RpPaymentDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return RpPaymentDetailsRow.localTextPrefix; }
        protected getService() { return RpPaymentDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            FacebookDetailsService.Sync({},
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
                    RpPaymentDetailsService.Sync({},
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