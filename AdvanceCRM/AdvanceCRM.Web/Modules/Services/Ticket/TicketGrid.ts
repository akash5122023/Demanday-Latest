

namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TicketGrid extends GridBase<TicketRow, any> {
        protected getColumnsKey() { return 'Services.Ticket'; }
        protected getDialogType() { return TicketDialog; }
        protected getIdProperty() { return TicketRow.idProperty; }
        protected getInsertPermission() { return TicketRow.insertPermission; }
        protected getLocalTextPrefix() { return TicketRow.localTextPrefix; }
        protected getService() { return TicketService.baseUrl; }

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