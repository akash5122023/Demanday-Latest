
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesFollowupsReportGrid extends GridBase<SalesFollowupsRow, any> {
        protected getColumnsKey() { return 'Sales.SalesFollowups'; }
        protected getDialogType() { return SalesFollowupsDialog; }
        protected getIdProperty() { return SalesFollowupsRow.idProperty; }
        protected getInsertPermission() { return SalesFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return SalesFollowupsRow.localTextPrefix; }
        protected getService() { return SalesFollowupsService.baseUrl; }


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