
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayMasterAccountGrid extends Serenity.EntityGrid<DemandayMasterAccountRow, any> {
        protected getColumnsKey() { return DemandayMasterAccountColumns.columnsKey; }
        protected getDialogType() { return DemandayMasterAccountDialog; }
        protected getIdProperty() { return DemandayMasterAccountRow.idProperty; }
        protected getInsertPermission() { return DemandayMasterAccountRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayMasterAccountRow.localTextPrefix; }
        protected getService() { return DemandayMasterAccountService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}