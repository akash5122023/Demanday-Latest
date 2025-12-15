
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIdiaMartGrid extends GridBase<BizMailIdiaMartRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailIdiaMart'; }
        protected getDialogType() { return BizMailIdiaMartDialog; }
        protected getIdProperty() { return BizMailIdiaMartRow.idProperty; }
        protected getInsertPermission() { return BizMailIdiaMartRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailIdiaMartRow.localTextPrefix; }
        protected getService() { return BizMailIdiaMartService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}