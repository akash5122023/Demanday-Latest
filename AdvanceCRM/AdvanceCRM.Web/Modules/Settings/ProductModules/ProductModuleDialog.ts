namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductModuleDialog extends Serenity.EntityDialog<ProductModuleRow, any> {
        protected getFormKey() { return ProductModuleForm.formKey; }
        protected getIdProperty() { return ProductModuleRow.idProperty; }
        protected getLocalTextPrefix() { return ProductModuleRow.localTextPrefix; }
        protected getNameProperty() { return ProductModuleRow.nameProperty; }
        protected getService() { return ProductModulesService.baseUrl; }
        protected getDeletePermission() { return ProductModuleRow.deletePermission; }
        protected getInsertPermission() { return ProductModuleRow.insertPermission; }
        protected getUpdatePermission() { return ProductModuleRow.updatePermission; }

        protected form = new ProductModuleForm(this.idPrefix);
    }
}
