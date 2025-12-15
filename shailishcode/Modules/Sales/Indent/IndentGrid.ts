namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class IndentGrid extends GridBase<IndentRow, any> {
        protected getColumnsKey() { return 'Sales.Indent'; }
        protected getDialogType() { return IndentDialog; }
        protected getIdProperty() { return IndentRow.idProperty; }
        protected getInsertPermission() { return IndentRow.insertPermission; }
        protected getLocalTextPrefix() { return IndentRow.localTextPrefix; }
        protected getService() { return IndentService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}