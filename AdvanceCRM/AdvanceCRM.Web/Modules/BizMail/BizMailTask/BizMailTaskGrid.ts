
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailTaskGrid extends GridBase<BizMailTaskRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailTask'; }
        protected getDialogType() { return BizMailTaskDialog; }
        protected getIdProperty() { return BizMailTaskRow.idProperty; }
        protected getInsertPermission() { return BizMailTaskRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailTaskRow.localTextPrefix; }
        protected getService() { return BizMailTaskService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}