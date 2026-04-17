
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class DemandayCompetitorGrid extends GridBase<DemandayCompetitorRow, any> {
        protected getColumnsKey() { return "Toolkit.DemandayCompetitor"; }
        protected getDialogType() { return DemandayCompetitorDialog; }
        protected getIdProperty() { return DemandayCompetitorRow.idProperty; }
        protected getInsertPermission() { return DemandayCompetitorRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayCompetitorRow.localTextPrefix; }
        protected getService() { return DemandayCompetitorService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = DemandayCompetitorRow.Fields;

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