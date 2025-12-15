
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesReturnGrid extends GridBase<SalesReturnRow, any> {
        protected getColumnsKey() { return 'Sales.SalesReturn'; }
        protected getDialogType() { return SalesReturnDialog; }
        protected getIdProperty() { return SalesReturnRow.idProperty; }
        protected getInsertPermission() { return SalesReturnRow.insertPermission; }
        protected getLocalTextPrefix() { return SalesReturnRow.localTextPrefix; }
        protected getService() { return SalesReturnService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}