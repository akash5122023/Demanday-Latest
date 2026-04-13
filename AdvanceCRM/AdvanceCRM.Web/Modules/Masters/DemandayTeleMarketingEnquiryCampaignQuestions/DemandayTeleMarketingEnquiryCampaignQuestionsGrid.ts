
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryCampaignQuestionsGrid extends GridBase<DemandayTeleMarketingEnquiryCampaignQuestionsRow, any> {
        protected getColumnsKey() { return "Masters.DemandayTeleMarketingEnquiryCampaignQuestions"; }
        protected getDialogType() { return DemandayTeleMarketingEnquiryCampaignQuestionsDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryCampaignQuestionsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}