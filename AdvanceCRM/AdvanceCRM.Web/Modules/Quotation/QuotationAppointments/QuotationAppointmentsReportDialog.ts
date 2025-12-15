
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationAppointmentsReportDialog extends ReadOnlyDialog<QuotationAppointmentsRow> {
        protected getFormKey() { return QuotationAppointmentsForm.formKey; }
        protected getIdProperty() { return QuotationAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return QuotationAppointmentsRow.nameProperty; }
        protected getService() { return QuotationAppointmentsService.baseUrl; }
        protected getDeletePermission() { return QuotationAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return QuotationAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return QuotationAppointmentsRow.updatePermission; }

        protected form = new QuotationAppointmentsForm(this.idPrefix);

    }
}