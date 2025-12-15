
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class InstamojoGrid extends GridBase<InstamojoRow, any> {
        protected getColumnsKey() { return 'Settings.Instamojo'; }
        protected getDialogType() { return InstamojoDialog; }
        protected getIdProperty() { return InstamojoRow.idProperty; }
        protected getInsertPermission() { return InstamojoRow.insertPermission; }
        protected getLocalTextPrefix() { return InstamojoRow.localTextPrefix; }
        protected getService() { return InstamojoService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}