
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailContactGrid extends GridBase<BizMailContactRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailContact'; }
        protected getDialogType() { return BizMailContactDialog; }
        protected getIdProperty() { return BizMailContactRow.idProperty; }
        protected getInsertPermission() { return BizMailContactRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailContactRow.localTextPrefix; }
        protected getService() { return BizMailContactService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}