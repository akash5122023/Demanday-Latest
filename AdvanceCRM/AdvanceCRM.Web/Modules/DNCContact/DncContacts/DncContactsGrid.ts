
namespace AdvanceCRM.DNCContact {

    @Serenity.Decorators.registerClass()
    export class DncContactsGrid extends GridBase<DncContactsRow, any> {
        protected getColumnsKey() { return DncContactsColumns.columnsKey; }
        protected getDialogType() { return DncContactsDialog; }
        protected getIdProperty() { return DncContactsRow.idProperty; }
        protected getInsertPermission() { return DncContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return DncContactsRow.localTextPrefix; }
        protected getService() { return DncContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = DncContactsRow.Fields;

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

            return this.orderQuickFilters(filters);
        }
    }
}