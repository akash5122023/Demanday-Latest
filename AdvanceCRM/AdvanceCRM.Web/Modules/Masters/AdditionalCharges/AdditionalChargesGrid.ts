

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalChargesGrid extends GridBase<AdditionalChargesRow, any> {
        protected getColumnsKey() { return 'Masters.AdditionalCharges'; }
        protected getDialogType() { return AdditionalChargesDialog; }
        protected getIdProperty() { return AdditionalChargesRow.idProperty; }
        protected getInsertPermission() { return AdditionalChargesRow.insertPermission; }
        protected getLocalTextPrefix() { return AdditionalChargesRow.localTextPrefix; }
        protected getService() { return AdditionalChargesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}