
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryAppointmentsReportGrid extends GridBase<EnquiryAppointmentsRow, any> {
        protected getColumnsKey() { return 'Enquiry.EnquiryAppointments'; }
        protected getIdProperty() { return EnquiryAppointmentsRow.idProperty; }
        protected getInsertPermission() { return EnquiryAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryAppointmentsRow.localTextPrefix; }
        protected getService() { return EnquiryAppointmentsService.baseUrl; }

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