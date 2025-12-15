

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class SourceGrid extends GridBase<SourceRow, any> {
        protected getColumnsKey() { return 'Masters.Source'; }
        protected getDialogType() { return SourceDialog; }
        protected getIdProperty() { return SourceRow.idProperty; }
        protected getInsertPermission() { return SourceRow.insertPermission; }
        protected getLocalTextPrefix() { return SourceRow.localTextPrefix; }
        protected getService() { return SourceService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}