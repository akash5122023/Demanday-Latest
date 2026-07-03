
namespace AdvanceCRM.DNCContact {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DncContactsDialog extends DialogBase<DncContactsRow, any> {
        protected getFormKey() { return DncContactsForm.formKey; }
        protected getIdProperty() { return DncContactsRow.idProperty; }
        protected getLocalTextPrefix() { return DncContactsRow.localTextPrefix; }
        protected getNameProperty() { return DncContactsRow.nameProperty; }
        protected getService() { return DncContactsService.baseUrl; }
        protected getDeletePermission() { return DncContactsRow.deletePermission; }
        protected getInsertPermission() { return DncContactsRow.insertPermission; }
        protected getUpdatePermission() { return DncContactsRow.updatePermission; }

        protected form = new DncContactsForm(this.idPrefix);

    }
}