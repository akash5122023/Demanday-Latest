
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class RejectionOutwardGrid extends GridBase<RejectionOutwardRow, any> {
        protected getColumnsKey() { return 'Purchase.RejectionOutward'; }
        protected getDialogType() { return RejectionOutwardDialog; }
        protected getIdProperty() { return RejectionOutwardRow.idProperty; }
        protected getInsertPermission() { return RejectionOutwardRow.insertPermission; }
        protected getLocalTextPrefix() { return RejectionOutwardRow.localTextPrefix; }
        protected getService() { return RejectionOutwardService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}