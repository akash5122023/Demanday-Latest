
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class EnquiryContactsDialog extends DialogBase<EnquiryContactsRow, any> {
        protected getFormKey() { return EnquiryContactsForm.formKey; }
        protected getIdProperty() { return EnquiryContactsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryContactsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryContactsRow.nameProperty; }
        protected getService() { return EnquiryContactsService.baseUrl; }
        protected getDeletePermission() { return EnquiryContactsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryContactsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryContactsRow.updatePermission; }

        protected form = new EnquiryContactsForm(this.idPrefix);

    }
}