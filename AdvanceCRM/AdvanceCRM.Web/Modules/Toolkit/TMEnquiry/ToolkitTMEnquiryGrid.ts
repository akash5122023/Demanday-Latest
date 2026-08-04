
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

            return this.orderQuickFilters(filters);
        }
    }
}
