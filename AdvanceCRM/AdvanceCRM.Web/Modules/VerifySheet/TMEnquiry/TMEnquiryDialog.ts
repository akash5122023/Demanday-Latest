
namespace AdvanceCRM.VerifySheet {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class TMEnquiryDialog extends DialogBase<TMEnquiryRow, any> {
        protected getFormKey() { return TMEnquiryForm.formKey; }
        protected getIdProperty() { return TMEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return TMEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return TMEnquiryRow.nameProperty; }
        protected getService() { return TMEnquiryService.baseUrl; }
        protected getDeletePermission() { return TMEnquiryRow.deletePermission; }
        protected getInsertPermission() { return TMEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return TMEnquiryRow.updatePermission; }

        protected form = new TMEnquiryForm(this.idPrefix);

    }
}
