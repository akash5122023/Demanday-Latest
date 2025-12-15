
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class LogInOutLogDialog extends ReadOnlyDialog<LogInOutLogRow> {
        protected getFormKey() { return LogInOutLogForm.formKey; }
        protected getIdProperty() { return LogInOutLogRow.idProperty; }
        protected getLocalTextPrefix() { return LogInOutLogRow.localTextPrefix; }
        protected getService() { return LogInOutLogService.baseUrl; }
        protected getDeletePermission() { return LogInOutLogRow.deletePermission; }
        protected getInsertPermission() { return LogInOutLogRow.insertPermission; }
        protected getUpdatePermission() { return LogInOutLogRow.updatePermission; }

        protected form = new LogInOutLogForm(this.idPrefix);

    }
}