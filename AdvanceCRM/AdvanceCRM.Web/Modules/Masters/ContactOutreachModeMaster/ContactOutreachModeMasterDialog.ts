
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ContactOutreachModeMasterDialog extends Serenity.EntityDialog<ContactOutreachModeMasterRow, any> {
        protected getFormKey() { return ContactOutreachModeMasterForm.formKey; }
        protected getIdProperty() { return ContactOutreachModeMasterRow.idProperty; }
        protected getLocalTextPrefix() { return ContactOutreachModeMasterRow.localTextPrefix; }
        protected getNameProperty() { return ContactOutreachModeMasterRow.nameProperty; }
        protected getService() { return ContactOutreachModeMasterService.baseUrl; }
        protected getDeletePermission() { return ContactOutreachModeMasterRow.deletePermission; }
        protected getInsertPermission() { return ContactOutreachModeMasterRow.insertPermission; }
        protected getUpdatePermission() { return ContactOutreachModeMasterRow.updatePermission; }

        protected form = new ContactOutreachModeMasterForm(this.idPrefix);

    }
}