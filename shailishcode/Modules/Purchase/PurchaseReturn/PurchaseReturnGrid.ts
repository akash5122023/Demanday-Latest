
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class PurchaseReturnGrid extends GridBase<PurchaseReturnRow, any> {
        protected getColumnsKey() { return 'Purchase.PurchaseReturn'; }
        protected getDialogType() { return PurchaseReturnDialog; }
        protected getIdProperty() { return PurchaseReturnRow.idProperty; }
        protected getInsertPermission() { return PurchaseReturnRow.insertPermission; }
        protected getLocalTextPrefix() { return PurchaseReturnRow.localTextPrefix; }
        protected getService() { return PurchaseReturnService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}