
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.filterable()

    export class LogInOutLogGrid extends Serenity.EntityGrid<LogInOutLogRow, any> {
        protected getColumnsKey() { return 'Administration.LogInOutLog'; }
        protected getDialogType() { return LogInOutLogDialog; }
        protected getIdProperty() { return LogInOutLogRow.idProperty; }
        protected getInsertPermission() { return LogInOutLogRow.insertPermission; }
        protected getLocalTextPrefix() { return LogInOutLogRow.localTextPrefix; }
        protected getService() { return LogInOutLogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            return buttons
        }
    }
}