
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayEnquiryGrid extends GridBase<DemandayEnquiryRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayEnquiry' }
        protected getDialogType() { return DemandayEnquiryDialog; }
        protected getIdProperty() { return DemandayEnquiryRow.idProperty; }
        protected getInsertPermission() { return DemandayEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayEnquiryRow.localTextPrefix; }
        protected getService() { return DemandayEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}