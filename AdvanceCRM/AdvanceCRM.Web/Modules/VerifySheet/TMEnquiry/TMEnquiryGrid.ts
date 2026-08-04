
namespace AdvanceCRM.VerifySheet {

    @Serenity.Decorators.registerClass()
    export class TMEnquiryGrid extends GridBase<TMEnquiryRow, any> {
        protected getColumnsKey() { return TMEnquiryColumns.columnsKey; }
        protected getDialogType() { return TMEnquiryDialog; }
        protected getIdProperty() { return TMEnquiryRow.idProperty; }
        protected getInsertPermission() { return TMEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return TMEnquiryRow.localTextPrefix; }
        protected getService() { return TMEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = TMEnquiryRow.Fields;

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayMasterAccount"
                },
                field: fld.MasterAccountId,
                title: 'Master Account'
            });

            filters.push({
                type: Serenity.StringEditor,
                field: fld.CampaignId,
                title: 'Campaign Id'
            });

            return this.orderQuickFilters(filters);
        }
    }
}
