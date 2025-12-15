
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class AppointmentSmsLogGrid extends Serenity.EntityGrid<AppointmentSmsLogRow, any> {
        protected getColumnsKey() { return 'Administration.AppointmentSmsLog'; }
        protected getDialogType() { return AppointmentSmsLogDialog; }
        protected getIdProperty() { return AppointmentSmsLogRow.idProperty; }
        protected getInsertPermission() { return AppointmentSmsLogRow.insertPermission; }
        protected getLocalTextPrefix() { return AppointmentSmsLogRow.localTextPrefix; }
        protected getService() { return AppointmentSmsLogService.baseUrl; }

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