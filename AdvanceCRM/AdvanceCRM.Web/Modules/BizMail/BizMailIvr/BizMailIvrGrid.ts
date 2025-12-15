
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIvrGrid extends GridBase<BizMailIvrRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailIvr'; }
        protected getDialogType() { return BizMailIvrDialog; }
        protected getIdProperty() { return BizMailIvrRow.idProperty; }
        protected getInsertPermission() { return BizMailIvrRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailIvrRow.localTextPrefix; }
        protected getService() { return BizMailIvrService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}