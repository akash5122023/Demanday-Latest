
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceAppointmentsReportDialog extends ReadOnlyDialog<InvoiceAppointmentsRow> {
        protected getFormKey() { return InvoiceAppointmentsForm.formKey; }
        protected getIdProperty() { return InvoiceAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceAppointmentsRow.nameProperty; }
        protected getService() { return InvoiceAppointmentsService.baseUrl; }
        protected getDeletePermission() { return InvoiceAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return InvoiceAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return InvoiceAppointmentsRow.updatePermission; }

        protected form = new InvoiceAppointmentsForm(this.idPrefix);

    }
}