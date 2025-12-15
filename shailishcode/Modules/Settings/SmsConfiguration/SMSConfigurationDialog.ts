
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class SMSConfigurationDialog extends ConfigurationDialog<SMSConfigurationRow> {
        protected getFormKey() { return SMSConfigurationForm.formKey; }
        protected getIdProperty() { return SMSConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return SMSConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return SMSConfigurationRow.nameProperty; }
        protected getService() { return SMSConfigurationService.baseUrl; }
        protected getDeletePermission() { return SMSConfigurationRow.deletePermission; }
        protected getInsertPermission() { return SMSConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return SMSConfigurationRow.updatePermission; }

        protected form = new SMSConfigurationForm(this.idPrefix);

    }
}