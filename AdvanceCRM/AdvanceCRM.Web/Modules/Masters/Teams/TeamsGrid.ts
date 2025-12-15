
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TeamsGrid extends GridBase<TeamsRow, any> {
        protected getColumnsKey() { return 'Masters.Teams'; }
        protected getDialogType() { return TeamsDialog; }
        protected getIdProperty() { return TeamsRow.idProperty; }
        protected getInsertPermission() { return TeamsRow.insertPermission; }
        protected getLocalTextPrefix() { return TeamsRow.localTextPrefix; }
        protected getService() { return TeamsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}