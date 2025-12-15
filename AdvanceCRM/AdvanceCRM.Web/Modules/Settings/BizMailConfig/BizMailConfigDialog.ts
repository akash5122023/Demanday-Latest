
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />

namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class BizMailConfigDialog extends ConfigurationDialog<BizMailConfigRow> {
        protected getFormKey() { return BizMailConfigForm.formKey; }
        protected getIdProperty() { return BizMailConfigRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailConfigRow.localTextPrefix; }
        protected getNameProperty() { return BizMailConfigRow.nameProperty; }
        protected getService() { return BizMailConfigService.baseUrl; }
        protected getDeletePermission() { return BizMailConfigRow.deletePermission; }
        protected getInsertPermission() { return BizMailConfigRow.insertPermission; }
        protected getUpdatePermission() { return BizMailConfigRow.updatePermission; }

        protected form = new BizMailConfigForm(this.idPrefix);
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

