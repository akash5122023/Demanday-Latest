
namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class QuotationProductsDialog extends DialogBase<QuotationProductsRow, any> {
        protected getFormKey() { return QuotationProductsForm.formKey; }
        protected getIdProperty() { return QuotationProductsRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationProductsRow.localTextPrefix; }
        protected getNameProperty() { return QuotationProductsRow.nameProperty; }
        protected getService() { return QuotationProductsService.baseUrl; }
        protected getDeletePermission() { return QuotationProductsRow.deletePermission; }
        protected getInsertPermission() { return QuotationProductsRow.insertPermission; }
        protected getUpdatePermission() { return QuotationProductsRow.updatePermission; }

        protected form = new QuotationProductsForm(this.idPrefix);

    }
}