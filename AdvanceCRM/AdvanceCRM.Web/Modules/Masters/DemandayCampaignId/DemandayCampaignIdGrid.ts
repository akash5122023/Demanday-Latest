
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayCampaignIdGrid extends Serenity.EntityGrid<DemandayCampaignIdRow, any> {
        protected getColumnsKey() { return DemandayCampaignIdColumns.columnsKey; }
        protected getDialogType() { return DemandayCampaignIdDialog; }
        protected getIdProperty() { return DemandayCampaignIdRow.idProperty; }
        protected getInsertPermission() { return DemandayCampaignIdRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayCampaignIdRow.localTextPrefix; }
        protected getService() { return DemandayCampaignIdService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}