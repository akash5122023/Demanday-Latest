
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailJustDialGrid extends GridBase<BizMailJustDialRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailJustDial'; }
        protected getDialogType() { return BizMailJustDialDialog; }
        protected getIdProperty() { return BizMailJustDialRow.idProperty; }
        protected getInsertPermission() { return BizMailJustDialRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailJustDialRow.localTextPrefix; }
        protected getService() { return BizMailJustDialService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}