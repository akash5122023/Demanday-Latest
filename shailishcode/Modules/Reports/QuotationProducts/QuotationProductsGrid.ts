
namespace AdvanceCRM.Reports {
    import fld = QuotationProductsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class QuotationProductsGrid extends GridBase<QuotationProductsRow, any> {
        protected getColumnsKey() { return 'Reports.QuotationProducts'; }
        protected getDialogType() { return QuotationProductsDialog; }
        protected getIdProperty() { return QuotationProductsRow.idProperty; }
        protected getInsertPermission() { return QuotationProductsRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationProductsRow.localTextPrefix; }
        protected getService() { return QuotationProductsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            this.view.setGrouping(
                [{
                    formatter: x => 'Quotation: ' + x.value + ' (' + x.count + ' items)',
                    getter: fld.QuotationId
                }])
        }
    }
}