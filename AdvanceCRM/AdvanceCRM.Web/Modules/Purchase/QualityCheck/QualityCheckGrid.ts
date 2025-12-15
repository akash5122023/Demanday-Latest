
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class QualityCheckGrid extends GridBase<QualityCheckRow, any> {
        protected getColumnsKey() { return 'Purchase.QualityCheck'; }
        protected getDialogType() { return QualityCheckDialog; }
        protected getIdProperty() { return QualityCheckRow.idProperty; }
        protected getInsertPermission() { return QualityCheckRow.insertPermission; }
        protected getLocalTextPrefix() { return QualityCheckRow.localTextPrefix; }
        protected getService() { return QualityCheckService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}