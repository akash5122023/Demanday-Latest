
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailWebGrid extends GridBase<BizMailWebRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailWeb'; }
        protected getDialogType() { return BizMailWebDialog; }
        protected getIdProperty() { return BizMailWebRow.idProperty; }
        protected getInsertPermission() { return BizMailWebRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailWebRow.localTextPrefix; }
        protected getService() { return BizMailWebService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}