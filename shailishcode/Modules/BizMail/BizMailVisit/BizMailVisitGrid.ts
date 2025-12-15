
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailVisitGrid extends GridBase<BizMailVisitRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailVisit'; }
        protected getDialogType() { return BizMailVisitDialog; }
        protected getIdProperty() { return BizMailVisitRow.idProperty; }
        protected getInsertPermission() { return BizMailVisitRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailVisitRow.localTextPrefix; }
        protected getService() { return BizMailVisitService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}