
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class IndiaMartConfigurationDialog extends ConfigurationDialog<IndiaMartConfigurationRow> {
        protected getFormKey() { return IndiaMartConfigurationForm.formKey; }
        protected getIdProperty() { return IndiaMartConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return IndiaMartConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return IndiaMartConfigurationRow.nameProperty; }
        protected getService() { return IndiaMartConfigurationService.baseUrl; }
        protected getDeletePermission() { return IndiaMartConfigurationRow.deletePermission; }
        protected getInsertPermission() { return IndiaMartConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return IndiaMartConfigurationRow.updatePermission; }

        protected form = new IndiaMartConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}