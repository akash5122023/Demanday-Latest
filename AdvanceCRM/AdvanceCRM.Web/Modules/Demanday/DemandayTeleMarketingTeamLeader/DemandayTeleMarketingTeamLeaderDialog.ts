
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingTeamLeaderDialog extends DialogBase<DemandayTeleMarketingTeamLeaderRow, any> {
        protected getFormKey() { return DemandayTeleMarketingTeamLeaderForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingTeamLeaderRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingTeamLeaderRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingTeamLeaderRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingTeamLeaderService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingTeamLeaderRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingTeamLeaderRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingTeamLeaderRow.updatePermission; }

        protected form = new DemandayTeleMarketingTeamLeaderForm(this.idPrefix);

        constructor() {
            super();

            this.form.CampaignId.changeSelect2(e => {
                this.form.QADetails.setCampaignId(this.form.CampaignId.value || null);
            });
        }

        protected afterLoadEntity(): void {
            super.afterLoadEntity();
            var row = this.entity as DemandayTeleMarketingTeamLeaderRow;
            this.form.QADetails.setCampaignId(row ? row.CampaignId : null);
            DemandayAudioAttachment.render(this.element, this.idPrefix, "Attachments",
                row ? row.Attachments : null);
            if (!this.form.Date.value) {
                this.form.Date.value = Q.formatDate(new Date(), "yyyy-MM-dd HH:mm");
            }
        }
    }
}