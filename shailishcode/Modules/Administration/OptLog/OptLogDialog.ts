
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class OptLogDialog extends ReadOnlyDialog<OptLogRow> {
        protected getFormKey() { return OptLogForm.formKey; }
        protected getIdProperty() { return OptLogRow.idProperty; }
        protected getLocalTextPrefix() { return OptLogRow.localTextPrefix; }
        protected getNameProperty() { return OptLogRow.nameProperty; }
        protected getService() { return OptLogService.baseUrl; }
        protected getDeletePermission() { return OptLogRow.deletePermission; }
        protected getInsertPermission() { return OptLogRow.insertPermission; }
        protected getUpdatePermission() { return OptLogRow.updatePermission; }

        protected form = new OptLogForm(this.idPrefix);

    }
}