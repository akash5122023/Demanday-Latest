
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailFacebookGrid extends GridBase<BizMailFacebookRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailFacebook'; }
        protected getDialogType() { return BizMailFacebookDialog; }
        protected getIdProperty() { return BizMailFacebookRow.idProperty; }
        protected getInsertPermission() { return BizMailFacebookRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailFacebookRow.localTextPrefix; }
        protected getService() { return BizMailFacebookService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}