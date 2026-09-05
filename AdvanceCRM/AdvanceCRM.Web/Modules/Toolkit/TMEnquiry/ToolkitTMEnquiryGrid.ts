
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

        // Show up to 100000 rows on a single page (effectively "all" for this module).
        protected getViewOptions() {
            let opt = super.getViewOptions();
            opt.rowsPerPage = 100000;
            return opt;
        }

        // Make 100000 an actual, selected option in the pager's page-size dropdown
        // (otherwise the dropdown shows blank since 100000 isn't a default option).
        protected getPagerOptions() {
            let opt = super.getPagerOptions();
            opt.rowsPerPage = 100000;
            opt.rowsPerPageOptions = [2500, 5000, 10000, 50000, 100000];
            return opt;
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
