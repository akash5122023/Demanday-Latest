
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class RazorPayDialog extends ConfigurationDialog<RazorPayRow> {
        protected getFormKey() { return RazorPayForm.formKey; }
        protected getIdProperty() { return RazorPayRow.idProperty; }
        protected getLocalTextPrefix() { return RazorPayRow.localTextPrefix; }
        protected getNameProperty() { return RazorPayRow.nameProperty; }
        protected getService() { return RazorPayService.baseUrl; }
        protected getDeletePermission() { return RazorPayRow.deletePermission; }
        protected getInsertPermission() { return RazorPayRow.insertPermission; }
        protected getUpdatePermission() { return RazorPayRow.updatePermission; }

        protected form = new RazorPayForm(this.idPrefix);
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