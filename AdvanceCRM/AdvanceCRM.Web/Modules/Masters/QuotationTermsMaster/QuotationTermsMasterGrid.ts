

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class QuotationTermsMasterGrid extends GridBase<QuotationTermsMasterRow, any> {
        protected getColumnsKey() { return 'Masters.QuotationTermsMaster'; }
        protected getDialogType() { return QuotationTermsMasterDialog; }
        protected getIdProperty() { return QuotationTermsMasterRow.idProperty; }
        protected getInsertPermission() { return QuotationTermsMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationTermsMasterRow.localTextPrefix; }
        protected getService() { return QuotationTermsMasterService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}