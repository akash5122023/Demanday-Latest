
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class DailyWishesLogDialog extends ReadOnlyDialog<DailyWishesLogRow> {
        protected getFormKey() { return DailyWishesLogForm.formKey; }
        protected getIdProperty() { return DailyWishesLogRow.idProperty; }
        protected getLocalTextPrefix() { return DailyWishesLogRow.localTextPrefix; }
        protected getNameProperty() { return DailyWishesLogRow.nameProperty; }
        protected getService() { return DailyWishesLogService.baseUrl; }
        protected getDeletePermission() { return DailyWishesLogRow.deletePermission; }
        protected getInsertPermission() { return DailyWishesLogRow.insertPermission; }
        protected getUpdatePermission() { return DailyWishesLogRow.updatePermission; }

        protected form = new DailyWishesLogForm(this.idPrefix);

    }
}