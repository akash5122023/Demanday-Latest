
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsDivisionDialog extends DialogBase<ProductsDivisionRow, any> {
        protected getFormKey() { return ProductsDivisionForm.formKey; }
        protected getIdProperty() { return ProductsDivisionRow.idProperty; }
        protected getLocalTextPrefix() { return ProductsDivisionRow.localTextPrefix; }
        protected getNameProperty() { return ProductsDivisionRow.nameProperty; }
        protected getService() { return ProductsDivisionService.baseUrl; }
        protected getDeletePermission() { return ProductsDivisionRow.deletePermission; }
        protected getInsertPermission() { return ProductsDivisionRow.insertPermission; }
        protected getUpdatePermission() { return ProductsDivisionRow.updatePermission; }

        protected form = new ProductsDivisionForm(this.idPrefix);

    }
}