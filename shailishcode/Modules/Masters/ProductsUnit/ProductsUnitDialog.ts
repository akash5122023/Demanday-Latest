
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsUnitDialog extends DialogBase<ProductsUnitRow, any> {
        protected getFormKey() { return ProductsUnitForm.formKey; }
        protected getIdProperty() { return ProductsUnitRow.idProperty; }
        protected getLocalTextPrefix() { return ProductsUnitRow.localTextPrefix; }
        protected getNameProperty() { return ProductsUnitRow.nameProperty; }
        protected getService() { return ProductsUnitService.baseUrl; }
        protected getDeletePermission() { return ProductsUnitRow.deletePermission; }
        protected getInsertPermission() { return ProductsUnitRow.insertPermission; }
        protected getUpdatePermission() { return ProductsUnitRow.updatePermission; }

        protected form = new ProductsUnitForm(this.idPrefix);

    }
}