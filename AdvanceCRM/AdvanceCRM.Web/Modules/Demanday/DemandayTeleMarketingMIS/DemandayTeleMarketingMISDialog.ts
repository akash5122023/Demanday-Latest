
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingMISDialog extends DialogBase<DemandayTeleMarketingMISRow, any> {
        protected getFormKey() { return DemandayTeleMarketingMISForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingMISRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingMISRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingMISRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingMISService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingMISRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingMISRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingMISRow.updatePermission; }

        protected form = new DemandayTeleMarketingMISForm(this.idPrefix);

        constructor() {
            super();

            this.form.CampaignId.changeSelect2(e => {
                this.form.QADetails.setCampaignId(this.form.CampaignId.value || null);
            });
        }

        protected afterLoadEntity(): void {
            super.afterLoadEntity();
            var row = this.entity as DemandayTeleMarketingMISRow;
            this.form.QADetails.setCampaignId(row ? row.CampaignId : null);
            if (!this.form.Date.value) {
                this.form.Date.value = Q.formatDate(new Date(), "yyyy-MM-dd HH:mm");
            }
        }

    }
}