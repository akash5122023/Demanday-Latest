
namespace AdvanceCRM.Purchase {

    export class PurchaseGrid extends GridBase<PurchaseRow, any> {
        protected getColumnsKey() { return 'Purchase.Purchase'; }
        protected getDialogType() { return PurchaseDialog; }
        protected getIdProperty() { return PurchaseRow.idProperty; }
        protected getInsertPermission() { return PurchaseRow.insertPermission; }
        protected getLocalTextPrefix() { return PurchaseRow.localTextPrefix; }
        protected getService() { return PurchaseService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

      
    }
}