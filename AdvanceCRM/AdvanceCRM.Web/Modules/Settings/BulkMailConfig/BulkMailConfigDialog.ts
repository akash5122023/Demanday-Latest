/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />

namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class BulkMailConfigDialog extends ConfigurationDialog<BulkMailConfigRow> {
        protected getFormKey() { return BulkMailConfigForm.formKey; }
        protected getIdProperty() { return BulkMailConfigRow.idProperty; }
        protected getLocalTextPrefix() { return BulkMailConfigRow.localTextPrefix; }
        protected getNameProperty() { return BulkMailConfigRow.nameProperty; }
        protected getService() { return BulkMailConfigService.baseUrl; }
        protected getDeletePermission() { return BulkMailConfigRow.deletePermission; }
        protected getInsertPermission() { return BulkMailConfigRow.insertPermission; }
        protected getUpdatePermission() { return BulkMailConfigRow.updatePermission; }

        protected form = new BulkMailConfigForm(this.idPrefix);
        constructor() {
            super();
        }

    //    loadEntity(entity: FacebookConfigurationRow) {
    //        super.loadEntity(entity);

    //        var getUrl = window.location;
    //        var baseUrl = getUrl.protocol + "//" + getUrl.host;
    //        baseUrl = baseUrl + "/api/Leads/Facebook?";
    //        this.form.API.value = baseUrl;
    //    }
    }
}