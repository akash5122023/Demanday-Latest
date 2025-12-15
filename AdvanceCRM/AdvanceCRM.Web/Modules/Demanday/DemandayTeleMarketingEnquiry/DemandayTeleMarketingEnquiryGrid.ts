
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryGrid extends GridBase<DemandayTeleMarketingEnquiryRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingEnquiry' }
        protected getDialogType() { return DemandayTeleMarketingEnquiryDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            return buttons;
        }
    }
}