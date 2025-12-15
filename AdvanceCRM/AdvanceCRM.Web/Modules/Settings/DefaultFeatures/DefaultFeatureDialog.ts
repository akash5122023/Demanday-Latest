namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class DefaultFeatureDialog extends Serenity.EntityDialog<DefaultFeatureRow, any> {
        protected getFormKey() { return DefaultFeatureForm.formKey; }
        protected getIdProperty() { return DefaultFeatureRow.idProperty; }
        protected getLocalTextPrefix() { return DefaultFeatureRow.localTextPrefix; }
        protected getNameProperty() { return DefaultFeatureRow.nameProperty; }
        protected getService() { return 'Settings/DefaultFeatures'; }
        protected getDeletePermission() { return DefaultFeatureRow.deletePermission; }
        protected getInsertPermission() { return DefaultFeatureRow.insertPermission; }
        protected getUpdatePermission() { return DefaultFeatureRow.updatePermission; }

        protected form = new DefaultFeatureForm(this.idPrefix);
    }
}
