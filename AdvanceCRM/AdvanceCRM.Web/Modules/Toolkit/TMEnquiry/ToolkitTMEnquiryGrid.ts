
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class ToolkitTMEnquiryGrid extends GridBase<ToolkitTMEnquiryRow, any> {
        protected getColumnsKey() { return ToolkitTMEnquiryColumns.columnsKey; }
        protected getDialogType() { return ToolkitTMEnquiryDialog; }
        protected getIdProperty() { return ToolkitTMEnquiryRow.idProperty; }
        protected getInsertPermission() { return ToolkitTMEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return ToolkitTMEnquiryRow.localTextPrefix; }
        protected getService() { return ToolkitTMEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = ToolkitTMEnquiryRow.Fields;

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayMasterAccount"
                },
                field: fld.MasterAccountId,
                title: 'Master Account'
            });

            // Campaign list narrows to the campaigns of the selected Master Account,
            // same as every other Tool Kit module.
            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayCampaignId",
                    cascadeFrom: fld.MasterAccountId,
                    cascadeField: "DemandayMasterAccountId"
                },
                field: fld.CampaignId,
                title: 'Campaign'
            });

            return this.orderQuickFilters(filters);
        }
    }
}
