
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class EnquiryContactsGrid extends GridBase<EnquiryContactsRow, any> {
        protected getColumnsKey() { return 'Demanday.EnquiryContacts' }
        protected getDialogType() { return EnquiryContactsDialog; }
        protected getIdProperty() { return EnquiryContactsRow.idProperty; }
        protected getInsertPermission() { return EnquiryContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryContactsRow.localTextPrefix; }
        protected getService() { return EnquiryContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            // Example: remove default add button
            buttons.shift();
            return buttons;
        }
    }
}