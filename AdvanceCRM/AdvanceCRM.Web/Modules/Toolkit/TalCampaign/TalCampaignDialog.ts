
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class TalCampaignDialog extends DialogBase<TalCampaignRow, any> {
        protected getFormKey() { return TalCampaignForm.formKey; }
        protected getIdProperty() { return TalCampaignRow.idProperty; }
        protected getLocalTextPrefix() { return TalCampaignRow.localTextPrefix; }
        protected getNameProperty() { return TalCampaignRow.nameProperty; }
        protected getService() { return TalCampaignService.baseUrl; }
        protected getDeletePermission() { return TalCampaignRow.deletePermission; }
        protected getInsertPermission() { return TalCampaignRow.insertPermission; }
        protected getUpdatePermission() { return TalCampaignRow.updatePermission; }

        protected form = new TalCampaignForm(this.idPrefix);

    }
}