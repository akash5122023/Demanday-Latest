
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmCampaignDialog extends DialogBase<BmCampaignRow, any> {
        protected getFormKey() { return BmCampaignForm.formKey; }
        protected getIdProperty() { return BmCampaignRow.idProperty; }
        protected getLocalTextPrefix() { return BmCampaignRow.localTextPrefix; }
        protected getNameProperty() { return BmCampaignRow.nameProperty; }
        protected getService() { return BmCampaignService.baseUrl; }
        protected getDeletePermission() { return BmCampaignRow.deletePermission; }
        protected getInsertPermission() { return BmCampaignRow.insertPermission; }
        protected getUpdatePermission() { return BmCampaignRow.updatePermission; }

        protected form = new BmCampaignForm(this.idPrefix);

    }
}