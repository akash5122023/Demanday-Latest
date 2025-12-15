/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class InteraktConfigDialog extends ConfigurationDialog<InteraktConfigRow> {
        protected getFormKey() { return InteraktConfigForm.formKey; }
        protected getIdProperty() { return InteraktConfigRow.idProperty; }
        protected getLocalTextPrefix() { return InteraktConfigRow.localTextPrefix; }
        protected getNameProperty() { return InteraktConfigRow.nameProperty; }
        protected getService() { return InteraktConfigService.baseUrl; }
        protected getDeletePermission() { return InteraktConfigRow.deletePermission; }
        protected getInsertPermission() { return InteraktConfigRow.insertPermission; }
        protected getUpdatePermission() { return InteraktConfigRow.updatePermission; }

        protected form = new InteraktConfigForm(this.idPrefix);
       

        constructor() {
            super();
        }
    }
}