
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ContactAccountStatusMasterDialog extends Serenity.EntityDialog<ContactAccountStatusMasterRow, any> {
        protected getFormKey() { return ContactAccountStatusMasterForm.formKey; }
        protected getIdProperty() { return ContactAccountStatusMasterRow.idProperty; }
        protected getLocalTextPrefix() { return ContactAccountStatusMasterRow.localTextPrefix; }
        protected getNameProperty() { return ContactAccountStatusMasterRow.nameProperty; }
        protected getService() { return ContactAccountStatusMasterService.baseUrl; }
        protected getDeletePermission() { return ContactAccountStatusMasterRow.deletePermission; }
        protected getInsertPermission() { return ContactAccountStatusMasterRow.insertPermission; }
        protected getUpdatePermission() { return ContactAccountStatusMasterRow.updatePermission; }

        protected form = new ContactAccountStatusMasterForm(this.idPrefix);

    }
}