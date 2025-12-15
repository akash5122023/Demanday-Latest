namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductPlanDialog extends Serenity.EntityDialog<ProductPlanRow, any> {
        protected getFormKey() { return ProductPlanForm.formKey; }
        protected getIdProperty() { return ProductPlanRow.idProperty; }
        protected getLocalTextPrefix() { return ProductPlanRow.localTextPrefix; }
        protected getNameProperty() { return ProductPlanRow.nameProperty; }
        protected getService() { return 'Settings/ProductPlans'; }
        protected getDeletePermission() { return ProductPlanRow.deletePermission; }
        protected getInsertPermission() { return ProductPlanRow.insertPermission; }
        protected getUpdatePermission() { return ProductPlanRow.updatePermission; }

        protected form = new ProductPlanForm(this.idPrefix);
    }
}
