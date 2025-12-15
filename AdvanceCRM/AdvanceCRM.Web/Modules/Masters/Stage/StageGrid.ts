

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class StageGrid extends GridBase<StageRow, any> {
        protected getColumnsKey() { return 'Masters.Stage'; }
        protected getDialogType() { return StageDialog; }
        protected getIdProperty() { return StageRow.idProperty; }
        protected getInsertPermission() { return StageRow.insertPermission; }
        protected getLocalTextPrefix() { return StageRow.localTextPrefix; }
        protected getService() { return StageService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}