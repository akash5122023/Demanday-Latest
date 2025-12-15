
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryAppointmentsReportDialog extends ReadOnlyDialog<EnquiryAppointmentsRow> {
        protected getFormKey() { return EnquiryAppointmentsForm.formKey; }
        protected getIdProperty() { return EnquiryAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryAppointmentsRow.nameProperty; }
        protected getService() { return EnquiryAppointmentsService.baseUrl; }
        protected getDeletePermission() { return EnquiryAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryAppointmentsRow.updatePermission; }

        protected form = new EnquiryAppointmentsForm(this.idPrefix);

    }
}