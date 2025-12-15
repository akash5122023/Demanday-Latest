
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class GrnTwoGrid extends GridBase<GrnTwoRow, any> {
        protected getColumnsKey() { return 'Purchase.GrnTwo'; }
        protected getDialogType() { return GrnTwoDialog; }
        protected getIdProperty() { return GrnTwoRow.idProperty; }
        protected getInsertPermission() { return GrnTwoRow.insertPermission; }
        protected getLocalTextPrefix() { return GrnTwoRow.localTextPrefix; }
        protected getService() { return GrnTwoService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}