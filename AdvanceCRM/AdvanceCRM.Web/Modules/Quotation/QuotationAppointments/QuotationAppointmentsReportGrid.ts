
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationAppointmentsReportGrid extends GridBase<QuotationAppointmentsRow, any> {
        protected getColumnsKey() { return 'Quotation.QuotationAppointments'; }
        protected getIdProperty() { return QuotationAppointmentsRow.idProperty; }
        protected getInsertPermission() { return QuotationAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationAppointmentsRow.localTextPrefix; }
        protected getService() { return QuotationAppointmentsService.baseUrl; }

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