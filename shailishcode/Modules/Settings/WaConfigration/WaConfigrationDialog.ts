/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class WaConfigrationDialog extends ConfigurationDialog<WaConfigrationRow> {
        protected getFormKey() { return WaConfigrationForm.formKey; }
        protected getIdProperty() { return WaConfigrationRow.idProperty; }
        protected getLocalTextPrefix() { return WaConfigrationRow.localTextPrefix; }
        protected getNameProperty() { return WaConfigrationRow.nameProperty; }
        protected getService() { return WaConfigrationService.baseUrl; }
        protected getDeletePermission() { return WaConfigrationRow.deletePermission; }
        protected getInsertPermission() { return WaConfigrationRow.insertPermission; }
        protected getUpdatePermission() { return WaConfigrationRow.updatePermission; }

        protected form = new WaConfigrationForm(this.idPrefix);
        constructor() {
            super();
        }
    }
}