
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ContactAccountTypeMasterDialog extends Serenity.EntityDialog<ContactAccountTypeMasterRow, any> {
        protected getFormKey() { return ContactAccountTypeMasterForm.formKey; }
        protected getIdProperty() { return ContactAccountTypeMasterRow.idProperty; }
        protected getLocalTextPrefix() { return ContactAccountTypeMasterRow.localTextPrefix; }
        protected getNameProperty() { return ContactAccountTypeMasterRow.nameProperty; }
        protected getService() { return ContactAccountTypeMasterService.baseUrl; }
        protected getDeletePermission() { return ContactAccountTypeMasterRow.deletePermission; }
        protected getInsertPermission() { return ContactAccountTypeMasterRow.insertPermission; }
        protected getUpdatePermission() { return ContactAccountTypeMasterRow.updatePermission; }

        protected form = new ContactAccountTypeMasterForm(this.idPrefix);

    }
}