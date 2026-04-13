
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class OpenCampaignDialog extends DialogBase<OpenCampaignRow, any> {
        protected getFormKey() { return OpenCampaignForm.formKey; }
        protected getIdProperty() { return OpenCampaignRow.idProperty; }
        protected getLocalTextPrefix() { return OpenCampaignRow.localTextPrefix; }
        protected getNameProperty() { return OpenCampaignRow.nameProperty; }
        protected getService() { return OpenCampaignService.baseUrl; }
        protected getDeletePermission() { return OpenCampaignRow.deletePermission; }
        protected getInsertPermission() { return OpenCampaignRow.insertPermission; }
        protected getUpdatePermission() { return OpenCampaignRow.updatePermission; }

        protected form = new OpenCampaignForm(this.idPrefix);

    }
}