
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class AppointmentSmsLogDialog extends ReadOnlyDialog<AppointmentSmsLogRow> {
        protected getFormKey() { return AppointmentSmsLogForm.formKey; }
        protected getIdProperty() { return AppointmentSmsLogRow.idProperty; }
        protected getLocalTextPrefix() { return AppointmentSmsLogRow.localTextPrefix; }
        protected getNameProperty() { return AppointmentSmsLogRow.nameProperty; }
        protected getService() { return AppointmentSmsLogService.baseUrl; }
        protected getDeletePermission() { return AppointmentSmsLogRow.deletePermission; }
        protected getInsertPermission() { return AppointmentSmsLogRow.insertPermission; }
        protected getUpdatePermission() { return AppointmentSmsLogRow.updatePermission; }

        protected form = new AppointmentSmsLogForm(this.idPrefix);

    }
}