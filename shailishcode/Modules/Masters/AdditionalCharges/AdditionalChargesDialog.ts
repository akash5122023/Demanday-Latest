
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalChargesDialog extends DialogBase<AdditionalChargesRow, any> {
        protected getFormKey() { return AdditionalChargesForm.formKey; }
        protected getIdProperty() { return AdditionalChargesRow.idProperty; }
        protected getLocalTextPrefix() { return AdditionalChargesRow.localTextPrefix; }
        protected getNameProperty() { return AdditionalChargesRow.nameProperty; }
        protected getService() { return AdditionalChargesService.baseUrl; }
        protected getDeletePermission() { return AdditionalChargesRow.deletePermission; }
        protected getInsertPermission() { return AdditionalChargesRow.insertPermission; }
        protected getUpdatePermission() { return AdditionalChargesRow.updatePermission; }

        protected form = new AdditionalChargesForm(this.idPrefix);

    }
}