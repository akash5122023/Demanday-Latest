
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class QuotationTemplateDialog extends ConfigurationDialog<QuotationTemplateRow> {
        protected getFormKey() { return QuotationTemplateForm.formKey; }
        protected getIdProperty() { return QuotationTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationTemplateRow.localTextPrefix; }
        protected getNameProperty() { return QuotationTemplateRow.nameProperty; }
        protected getService() { return QuotationTemplateService.baseUrl; }

        protected form = new QuotationTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}