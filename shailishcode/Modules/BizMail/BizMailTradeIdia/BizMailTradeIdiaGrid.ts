
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailTradeIdiaGrid extends GridBase<BizMailTradeIdiaRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailTradeIdia'; }
        protected getDialogType() { return BizMailTradeIdiaDialog; }
        protected getIdProperty() { return BizMailTradeIdiaRow.idProperty; }
        protected getInsertPermission() { return BizMailTradeIdiaRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailTradeIdiaRow.localTextPrefix; }
        protected getService() { return BizMailTradeIdiaService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}