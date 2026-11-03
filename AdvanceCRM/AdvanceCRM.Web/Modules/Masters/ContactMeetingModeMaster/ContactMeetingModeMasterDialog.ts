
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ContactMeetingModeMasterDialog extends Serenity.EntityDialog<ContactMeetingModeMasterRow, any> {
        protected getFormKey() { return ContactMeetingModeMasterForm.formKey; }
        protected getIdProperty() { return ContactMeetingModeMasterRow.idProperty; }
        protected getLocalTextPrefix() { return ContactMeetingModeMasterRow.localTextPrefix; }
        protected getNameProperty() { return ContactMeetingModeMasterRow.nameProperty; }
        protected getService() { return ContactMeetingModeMasterService.baseUrl; }
        protected getDeletePermission() { return ContactMeetingModeMasterRow.deletePermission; }
        protected getInsertPermission() { return ContactMeetingModeMasterRow.insertPermission; }
        protected getUpdatePermission() { return ContactMeetingModeMasterRow.updatePermission; }

        protected form = new ContactMeetingModeMasterForm(this.idPrefix);

    }
}