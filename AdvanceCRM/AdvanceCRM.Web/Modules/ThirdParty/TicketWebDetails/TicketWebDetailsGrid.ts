
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class TicketWebDetailsGrid extends GridBase<TicketWebDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.TicketWebDetails'; }
        protected getDialogType() { return TicketWebDetailsDialog; }
        protected getIdProperty() { return TicketWebDetailsRow.idProperty; }
        protected getInsertPermission() { return TicketWebDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return TicketWebDetailsRow.localTextPrefix; }
        protected getService() { return TicketWebDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}