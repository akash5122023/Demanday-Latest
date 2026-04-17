
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryCampaignQuestionsDialog extends DialogBase<DemandayTeleMarketingEnquiryCampaignQuestionsRow, any> {
        protected getFormKey() { return DemandayTeleMarketingEnquiryCampaignQuestionsForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingEnquiryCampaignQuestionsService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.updatePermission; }

        protected form = new DemandayTeleMarketingEnquiryCampaignQuestionsForm(this.idPrefix);

    }
}