
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ContactDispositionMasterDialog extends Serenity.EntityDialog<ContactDispositionMasterRow, any> {
        protected getFormKey() { return ContactDispositionMasterForm.formKey; }
        protected getIdProperty() { return ContactDispositionMasterRow.idProperty; }
        protected getLocalTextPrefix() { return ContactDispositionMasterRow.localTextPrefix; }
        protected getNameProperty() { return ContactDispositionMasterRow.nameProperty; }
        protected getService() { return ContactDispositionMasterService.baseUrl; }
        protected getDeletePermission() { return ContactDispositionMasterRow.deletePermission; }
        protected getInsertPermission() { return ContactDispositionMasterRow.insertPermission; }
        protected getUpdatePermission() { return ContactDispositionMasterRow.updatePermission; }

        protected form = new ContactDispositionMasterForm(this.idPrefix);

    }
}