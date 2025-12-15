

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class BankMasterGrid extends GridBase<BankMasterRow, any> {
        protected getColumnsKey() { return 'Masters.BankMaster'; }
        protected getDialogType() { return BankMasterDialog; }
        protected getIdProperty() { return BankMasterRow.idProperty; }
        protected getInsertPermission() { return BankMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return BankMasterRow.localTextPrefix; }
        protected getService() { return BankMasterService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}