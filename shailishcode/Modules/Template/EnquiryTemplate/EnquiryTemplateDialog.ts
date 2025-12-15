
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class EnquiryTemplateDialog extends ConfigurationDialog<EnquiryTemplateRow> {
        protected getFormKey() { return EnquiryTemplateForm.formKey; }
        protected getIdProperty() { return EnquiryTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryTemplateRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryTemplateRow.nameProperty; }
        protected getService() { return EnquiryTemplateService.baseUrl; }

        protected form = new EnquiryTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}