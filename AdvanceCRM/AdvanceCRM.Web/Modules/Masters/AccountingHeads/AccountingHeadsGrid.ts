

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AccountingHeadsGrid extends GridBase<AccountingHeadsRow, any> {
        protected getColumnsKey() { return 'Masters.AccountingHeads'; }
        protected getDialogType() { return AccountingHeadsDialog; }
        protected getIdProperty() { return AccountingHeadsRow.idProperty; }
        protected getInsertPermission() { return AccountingHeadsRow.insertPermission; }
        protected getLocalTextPrefix() { return AccountingHeadsRow.localTextPrefix; }
        protected getService() { return AccountingHeadsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}