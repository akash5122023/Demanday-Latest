
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsGroupDialog extends DialogBase<ProductsGroupRow, any> {
        protected getFormKey() { return ProductsGroupForm.formKey; }
        protected getIdProperty() { return ProductsGroupRow.idProperty; }
        protected getLocalTextPrefix() { return ProductsGroupRow.localTextPrefix; }
        protected getNameProperty() { return ProductsGroupRow.nameProperty; }
        protected getService() { return ProductsGroupService.baseUrl; }
        protected getDeletePermission() { return ProductsGroupRow.deletePermission; }
        protected getInsertPermission() { return ProductsGroupRow.insertPermission; }
        protected getUpdatePermission() { return ProductsGroupRow.updatePermission; }

        protected form = new ProductsGroupForm(this.idPrefix);

    }
}