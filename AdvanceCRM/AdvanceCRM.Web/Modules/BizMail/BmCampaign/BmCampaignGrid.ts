
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmCampaignGrid extends GridBase<BmCampaignRow, any> {
        protected getColumnsKey() { return 'BizMail.BmCampaign'; }
        protected getDialogType() { return BmCampaignDialog; }
        protected getIdProperty() { return BmCampaignRow.idProperty; }
        protected getInsertPermission() { return BmCampaignRow.insertPermission; }
        protected getLocalTextPrefix() { return BmCampaignRow.localTextPrefix; }
        protected getService() { return BmCampaignService.baseUrl; }

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
                    BmCampaignService.Sync({},
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