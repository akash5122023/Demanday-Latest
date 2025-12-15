namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductPlanModuleDialog extends Serenity.EntityDialog<ProductPlanModuleRow, any> {
        protected getFormKey() { return ProductPlanModuleForm.formKey; }
        protected getIdProperty() { return ProductPlanModuleRow.idProperty; }
        protected getLocalTextPrefix() { return ProductPlanModuleRow.localTextPrefix; }
        protected getService() { return ProductPlanModulesService.baseUrl; }
        protected getDeletePermission() { return ProductPlanModuleRow.deletePermission; }
        protected getInsertPermission() { return ProductPlanModuleRow.insertPermission; }
        protected getUpdatePermission() { return ProductPlanModuleRow.updatePermission; }

        protected form = new ProductPlanModuleForm(this.idPrefix);
    }
}
