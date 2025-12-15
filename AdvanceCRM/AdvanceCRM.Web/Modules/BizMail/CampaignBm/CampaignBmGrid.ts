
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class CampaignBmGrid extends GridBase<CampaignBmRow, any> {
        protected getColumnsKey() { return 'BizMail.CampaignBm'; }
        protected getDialogType() { return CampaignBmDialog; }
        protected getIdProperty() { return CampaignBmRow.idProperty; }
        protected getInsertPermission() { return CampaignBmRow.insertPermission; }
        protected getLocalTextPrefix() { return CampaignBmRow.localTextPrefix; }
        protected getService() { return CampaignBmService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            /*buttons.shift();*/

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync Campaigns',
                onClick: () => {
                    CampaignBmService.Sync({},
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