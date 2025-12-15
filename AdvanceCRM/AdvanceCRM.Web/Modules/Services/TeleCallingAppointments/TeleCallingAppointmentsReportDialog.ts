
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingAppointmentsReportDialog extends ReadOnlyDialog<TeleCallingAppointmentsRow> {
        protected getFormKey() { return TeleCallingAppointmentsForm.formKey; }
        protected getIdProperty() { return TeleCallingAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingAppointmentsRow.nameProperty; }
        protected getService() { return TeleCallingAppointmentsService.baseUrl; }
        protected getDeletePermission() { return TeleCallingAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return TeleCallingAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return TeleCallingAppointmentsRow.updatePermission; }

        protected form = new TeleCallingAppointmentsForm(this.idPrefix);

    }
}