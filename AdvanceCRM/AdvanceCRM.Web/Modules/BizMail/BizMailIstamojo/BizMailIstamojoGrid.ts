
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIstamojoGrid extends GridBase<BizMailIstamojoRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailIstamojo'; }
        protected getDialogType() { return BizMailIstamojoDialog; }
        protected getIdProperty() { return BizMailIstamojoRow.idProperty; }
        protected getInsertPermission() { return BizMailIstamojoRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailIstamojoRow.localTextPrefix; }
        protected getService() { return BizMailIstamojoService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}