
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailEnquiryGrid extends GridBase<BizMailEnquiryRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailEnquiry'; }
        protected getDialogType() { return BizMailEnquiryDialog; }
        protected getIdProperty() { return BizMailEnquiryRow.idProperty; }
        protected getInsertPermission() { return BizMailEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailEnquiryRow.localTextPrefix; }
        protected getService() { return BizMailEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}