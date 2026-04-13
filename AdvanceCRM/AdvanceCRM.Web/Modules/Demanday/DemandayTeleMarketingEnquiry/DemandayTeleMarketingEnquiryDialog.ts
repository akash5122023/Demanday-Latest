
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingEnquiryDialog extends DialogBase<DemandayTeleMarketingEnquiryRow, any> {
        protected getFormKey() { return DemandayTeleMarketingEnquiryForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingEnquiryRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingEnquiryService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingEnquiryRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingEnquiryRow.updatePermission; }

        protected form = new DemandayTeleMarketingEnquiryForm(this.idPrefix);

        constructor() {
            super();

            this.form.CampaignId.changeSelect2(e => {
                this.form.QADetails.setCampaignId(this.form.CampaignId.value || null);
            });
        }

        protected afterLoadEntity(): void {
            super.afterLoadEntity();
            this.form.QADetails.setCampaignId(this.entity ? (this.entity as DemandayTeleMarketingEnquiryRow).CampaignId : null);
        }

    }
}