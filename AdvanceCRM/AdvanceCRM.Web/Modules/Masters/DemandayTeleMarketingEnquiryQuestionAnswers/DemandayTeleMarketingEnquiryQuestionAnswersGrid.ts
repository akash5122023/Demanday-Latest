
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryQuestionAnswersGrid extends GridBase<DemandayTeleMarketingEnquiryQuestionAnswersRow, any> {
        protected getColumnsKey() { return "Masters.DemandayTeleMarketingEnquiryQuestionAnswers"; }
        protected getDialogType() { return DemandayTeleMarketingEnquiryQuestionAnswersDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryQuestionAnswersService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}