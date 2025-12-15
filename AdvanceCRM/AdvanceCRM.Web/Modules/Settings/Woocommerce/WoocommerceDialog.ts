/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class WoocommerceDialog extends ConfigurationDialog<WoocommerceRow> {
        protected getFormKey() { return WoocommerceForm.formKey; }
        protected getIdProperty() { return WoocommerceRow.idProperty; }
        protected getLocalTextPrefix() { return WoocommerceRow.localTextPrefix; }
        protected getNameProperty() { return WoocommerceRow.nameProperty; }
        protected getService() { return WoocommerceService.baseUrl; }
        protected getDeletePermission() { return WoocommerceRow.deletePermission; }
        protected getInsertPermission() { return WoocommerceRow.insertPermission; }
        protected getUpdatePermission() { return WoocommerceRow.updatePermission; }

        protected form = new WoocommerceForm(this.idPrefix);
        constructor() {
            super();
        }
        loadEntity(entity: WoocommerceRow) {
            super.loadEntity(entity);

            // var getUrl = window.location;
            // var baseUrl = getUrl.protocol + "//" + getUrl.host;
            // baseUrl = baseUrl + "/api/Leads/Facebook?";
            //// this.form.API.value = baseUrl;
        }
    }
}