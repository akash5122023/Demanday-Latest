
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class FacebookConfigurationDialog extends ConfigurationDialog<FacebookConfigurationRow> {
        protected getFormKey() { return FacebookConfigurationForm.formKey; }
        protected getIdProperty() { return FacebookConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return FacebookConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return FacebookConfigurationRow.nameProperty; }
        protected getService() { return FacebookConfigurationService.baseUrl; }
        protected getDeletePermission() { return FacebookConfigurationRow.deletePermission; }
        protected getInsertPermission() { return FacebookConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return FacebookConfigurationRow.updatePermission; }

        protected form = new FacebookConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }

        loadEntity(entity: FacebookConfigurationRow) {
            super.loadEntity(entity);

           // var getUrl = window.location;
           // var baseUrl = getUrl.protocol + "//" + getUrl.host;
           // baseUrl = baseUrl + "/api/Leads/Facebook?";
           //// this.form.API.value = baseUrl;
        }
    }
}