
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class DailyWishesLogGrid extends Serenity.EntityGrid<DailyWishesLogRow, any> {
        protected getColumnsKey() { return 'Administration.DailyWishesLog'; }
        protected getDialogType() { return DailyWishesLogDialog; }
        protected getIdProperty() { return DailyWishesLogRow.idProperty; }
        protected getInsertPermission() { return DailyWishesLogRow.insertPermission; }
        protected getLocalTextPrefix() { return DailyWishesLogRow.localTextPrefix; }
        protected getService() { return DailyWishesLogService.baseUrl; }

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