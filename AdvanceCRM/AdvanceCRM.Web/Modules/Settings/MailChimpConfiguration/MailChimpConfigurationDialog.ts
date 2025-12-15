
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class MailChimpConfigurationDialog extends ConfigurationDialog<MailChimpConfigurationRow> {
        protected getFormKey() { return MailChimpConfigurationForm.formKey; }
        protected getIdProperty() { return MailChimpConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return MailChimpConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return MailChimpConfigurationRow.nameProperty; }
        protected getService() { return MailChimpConfigurationService.baseUrl; }
        protected getDeletePermission() { return MailChimpConfigurationRow.deletePermission; }
        protected getInsertPermission() { return MailChimpConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return MailChimpConfigurationRow.updatePermission; }

        protected form = new MailChimpConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}