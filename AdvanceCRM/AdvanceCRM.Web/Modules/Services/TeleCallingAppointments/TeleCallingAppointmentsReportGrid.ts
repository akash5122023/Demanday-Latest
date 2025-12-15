
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingAppointmentsReportGrid extends GridBase<TeleCallingAppointmentsRow, any> {
        protected getColumnsKey() { return 'Services.TeleCallingAppointments'; }
        protected getDialogType() { return TeleCallingAppointmentsReportDialog; }
        protected getIdProperty() { return TeleCallingAppointmentsRow.idProperty; }
        protected getInsertPermission() { return TeleCallingAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleCallingAppointmentsRow.localTextPrefix; }
        protected getService() { return TeleCallingAppointmentsService.baseUrl; }

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