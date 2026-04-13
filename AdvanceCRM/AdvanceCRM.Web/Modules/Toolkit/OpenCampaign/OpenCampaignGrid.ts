
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class OpenCampaignGrid extends GridBase<OpenCampaignRow, any> {
        protected getColumnsKey() { return "Toolkit.OpenCampaign" }
        protected getDialogType() { return OpenCampaignDialog; }
        protected getIdProperty() { return OpenCampaignRow.idProperty; }
        protected getInsertPermission() { return OpenCampaignRow.insertPermission; }
        protected getLocalTextPrefix() { return OpenCampaignRow.localTextPrefix; }
        protected getService() { return OpenCampaignService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = OpenCampaignRow.Fields;

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayMasterAccount"
                },
                field: fld.MasterAccountId,
                title: 'Master Account'
            });

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

            return filters;
        }
    }
}