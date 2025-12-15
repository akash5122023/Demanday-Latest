
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailCmsGrid extends GridBase<BizMailCmsRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailCms'; }
        protected getDialogType() { return BizMailCmsDialog; }
        protected getIdProperty() { return BizMailCmsRow.idProperty; }
        protected getInsertPermission() { return BizMailCmsRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailCmsRow.localTextPrefix; }
        protected getService() { return BizMailCmsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}