/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />

namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class BizWaConfigDialog extends ConfigurationDialog<BizWaConfigRow> {
        protected getFormKey() { return BizWaConfigForm.formKey; }
        protected getIdProperty() { return BizWaConfigRow.idProperty; }
        protected getLocalTextPrefix() { return BizWaConfigRow.localTextPrefix; }
        protected getNameProperty() { return BizWaConfigRow.nameProperty; }
        protected getService() { return BizWaConfigService.baseUrl; }
        protected getDeletePermission() { return BizWaConfigRow.deletePermission; }
        protected getInsertPermission() { return BizWaConfigRow.insertPermission; }
        protected getUpdatePermission() { return BizWaConfigRow.updatePermission; }

        protected form = new BizMailConfigForm(this.idPrefix);
        constructor() {
            super();
        }

    }
}