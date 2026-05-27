
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingQualiltyDialog extends DialogBase<DemandayTeleMarketingQualiltyRow, any> {
        protected getFormKey() { return DemandayTeleMarketingQualiltyForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingQualiltyRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingQualiltyRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingQualiltyRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingQualiltyService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingQualiltyRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingQualiltyRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingQualiltyRow.updatePermission; }

        protected form = new DemandayTeleMarketingQualiltyForm(this.idPrefix);

        constructor() {
            super();

            this.form.CampaignId.changeSelect2(e => {
                this.form.QADetails.setCampaignId(this.form.CampaignId.value || null);
            });
        }

        protected afterLoadEntity(): void {
            super.afterLoadEntity();
            var row = this.entity as DemandayTeleMarketingQualiltyRow;
            this.form.QADetails.setCampaignId(row ? row.CampaignId : null);
            DemandayAudioAttachment.render(this.element, this.idPrefix, "Attachments",
                row ? row.Attachments : null);
            if (!this.form.Date.value) {
                this.form.Date.value = Q.formatDate(new Date(), "yyyy-MM-dd HH:mm");
            }
        }
    }
}