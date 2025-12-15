
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalConcessionDialog extends DialogBase<AdditionalConcessionRow, any> {
        protected getFormKey() { return AdditionalConcessionForm.formKey; }
        protected getIdProperty() { return AdditionalConcessionRow.idProperty; }
        protected getLocalTextPrefix() { return AdditionalConcessionRow.localTextPrefix; }
        protected getNameProperty() { return AdditionalConcessionRow.nameProperty; }
        protected getService() { return AdditionalConcessionService.baseUrl; }
        protected getDeletePermission() { return AdditionalConcessionRow.deletePermission; }
        protected getInsertPermission() { return AdditionalConcessionRow.insertPermission; }
        protected getUpdatePermission() { return AdditionalConcessionRow.updatePermission; }

        protected form = new AdditionalConcessionForm(this.idPrefix);

    }
}