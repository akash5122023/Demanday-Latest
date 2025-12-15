
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaxDialog extends DialogBase<TaxRow, any> {
        protected getFormKey() { return TaxForm.formKey; }
        protected getIdProperty() { return TaxRow.idProperty; }
        protected getLocalTextPrefix() { return TaxRow.localTextPrefix; }
        protected getNameProperty() { return TaxRow.nameProperty; }
        protected getService() { return TaxService.baseUrl; }
        protected getDeletePermission() { return TaxRow.deletePermission; }
        protected getInsertPermission() { return TaxRow.insertPermission; }
        protected getUpdatePermission() { return TaxRow.updatePermission; }

        protected form = new TaxForm(this.idPrefix);

    }
}