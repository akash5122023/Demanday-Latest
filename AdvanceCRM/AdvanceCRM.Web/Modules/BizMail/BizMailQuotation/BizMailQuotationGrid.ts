
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailQuotationGrid extends GridBase<BizMailQuotationRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailQuotation'; }
        protected getDialogType() { return BizMailQuotationDialog; }
        protected getIdProperty() { return BizMailQuotationRow.idProperty; }
        protected getInsertPermission() { return BizMailQuotationRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailQuotationRow.localTextPrefix; }
        protected getService() { return BizMailQuotationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}