
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceAppointmentsReportGrid extends GridBase<InvoiceAppointmentsRow, any> {
        protected getColumnsKey() { return 'Sales.InvoiceAppointments'; }
        protected getDialogType() { return InvoiceAppointmentsDialog; }
        protected getIdProperty() { return InvoiceAppointmentsRow.idProperty; }
        protected getInsertPermission() { return InvoiceAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return InvoiceAppointmentsRow.localTextPrefix; }
        protected getService() { return InvoiceAppointmentsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            return buttons;
        }
    }
}