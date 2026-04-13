
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryQuestionAnswersDialog extends DialogBase<DemandayTeleMarketingEnquiryQuestionAnswersRow, any> {
        protected getFormKey() { return DemandayTeleMarketingEnquiryQuestionAnswersForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingEnquiryQuestionAnswersService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingEnquiryQuestionAnswersRow.updatePermission; }

        protected form = new DemandayTeleMarketingEnquiryQuestionAnswersForm(this.idPrefix);

    }
}