
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class BankMasterDialog extends DialogBase<BankMasterRow, any> {
        protected getFormKey() { return BankMasterForm.formKey; }
        protected getIdProperty() { return BankMasterRow.idProperty; }
        protected getLocalTextPrefix() { return BankMasterRow.localTextPrefix; }
        protected getNameProperty() { return BankMasterRow.nameProperty; }
        protected getService() { return BankMasterService.baseUrl; }
        protected getDeletePermission() { return BankMasterRow.deletePermission; }
        protected getInsertPermission() { return BankMasterRow.insertPermission; }
        protected getUpdatePermission() { return BankMasterRow.updatePermission; }

        protected form = new BankMasterForm(this.idPrefix);

    }
}