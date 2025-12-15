
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class OptLogGrid extends Serenity.EntityGrid<OptLogRow, any> {
        protected getColumnsKey() { return 'Administration.OptLog'; }
        protected getDialogType() { return OptLogDialog; }
        protected getIdProperty() { return OptLogRow.idProperty; }
        protected getInsertPermission() { return OptLogRow.insertPermission; }
        protected getLocalTextPrefix() { return OptLogRow.localTextPrefix; }
        protected getService() { return OptLogService.baseUrl; }

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