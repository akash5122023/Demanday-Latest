
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class InvoiceTemplateDialog extends ConfigurationDialog<InvoiceTemplateRow> {
        protected getFormKey() { return InvoiceTemplateForm.formKey; }
        protected getIdProperty() { return InvoiceTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceTemplateRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceTemplateRow.nameProperty; }
        protected getService() { return InvoiceTemplateService.baseUrl; }

        protected form = new InvoiceTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}