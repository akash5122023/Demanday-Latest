
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class QuotationTermsMasterDialog extends DialogBase<QuotationTermsMasterRow, any> {
        protected getFormKey() { return QuotationTermsMasterForm.formKey; }
        protected getIdProperty() { return QuotationTermsMasterRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationTermsMasterRow.localTextPrefix; }
        protected getNameProperty() { return QuotationTermsMasterRow.nameProperty; }
        protected getService() { return QuotationTermsMasterService.baseUrl; }
        protected getDeletePermission() { return QuotationTermsMasterRow.deletePermission; }
        protected getInsertPermission() { return QuotationTermsMasterRow.insertPermission; }
        protected getUpdatePermission() { return QuotationTermsMasterRow.updatePermission; }

        protected form = new QuotationTermsMasterForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}