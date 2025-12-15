
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class WatiConfigDialog extends ConfigurationDialog<WatiConfigRow> {
        protected getFormKey() { return WatiConfigForm.formKey; }
        protected getIdProperty() { return WatiConfigRow.idProperty; }
        protected getLocalTextPrefix() { return WatiConfigRow.localTextPrefix; }
        protected getNameProperty() { return WatiConfigRow.nameProperty; }
        protected getService() { return WatiConfigService.baseUrl; }
        protected getDeletePermission() { return WatiConfigRow.deletePermission; }
        protected getInsertPermission() { return WatiConfigRow.insertPermission; }
        protected getUpdatePermission() { return WatiConfigRow.updatePermission; }

        protected form = new WatiConfigForm(this.idPrefix);
       
    }
}