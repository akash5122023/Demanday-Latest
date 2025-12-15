
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class AiConfigurationDialog extends Serenity.EntityDialog<AiConfigurationRow, any> {
        protected getFormKey() { return AiConfigurationForm.formKey; }
        protected getIdProperty() { return AiConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return AiConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return AiConfigurationRow.nameProperty; }
        protected getService() { return AiConfigurationService.baseUrl; }
        protected getDeletePermission() { return AiConfigurationRow.deletePermission; }
        protected getInsertPermission() { return AiConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return AiConfigurationRow.updatePermission; }

        protected form = new AiConfigurationForm(this.idPrefix);

    }
}