
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceFollowupsReportGrid extends GridBase<InvoiceFollowupsRow, any> {
        protected getColumnsKey() { return 'Sales.InvoiceFollowups'; }
        protected getDialogType() { return InvoiceFollowupsDialog; }
        protected getIdProperty() { return InvoiceFollowupsRow.idProperty; }
        protected getInsertPermission() { return InvoiceFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return InvoiceFollowupsRow.localTextPrefix; }
        protected getService() { return InvoiceFollowupsService.baseUrl; }


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