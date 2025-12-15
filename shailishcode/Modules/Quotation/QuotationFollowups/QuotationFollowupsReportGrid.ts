
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationFollowupsReportGrid extends GridBase<QuotationFollowupsRow, any> {
        protected getColumnsKey() { return 'Quotation.QuotationFollowups'; }
        protected getIdProperty() { return QuotationFollowupsRow.idProperty; }
        protected getInsertPermission() { return QuotationFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationFollowupsRow.localTextPrefix; }
        protected getService() { return QuotationFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();
            return buttons;
        }
    }
}