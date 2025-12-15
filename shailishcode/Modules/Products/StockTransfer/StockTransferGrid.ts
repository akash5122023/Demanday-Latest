
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class StockTransferGrid extends GridBase<StockTransferRow, any> {
        protected getColumnsKey() { return 'Products.StockTransfer'; }
        protected getDialogType() { return StockTransferDialog; }
        protected getIdProperty() { return StockTransferRow.idProperty; }
        protected getInsertPermission() { return StockTransferRow.insertPermission; }
        protected getLocalTextPrefix() { return StockTransferRow.localTextPrefix; }
        protected getService() { return StockTransferService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}