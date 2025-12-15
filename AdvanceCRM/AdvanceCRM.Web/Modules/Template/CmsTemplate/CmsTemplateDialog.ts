
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.panel()
    export class CmsTemplateDialog extends ConfigurationDialog<CmsTemplateRow> {
        protected getFormKey() { return CmsTemplateForm.formKey; }
        protected getIdProperty() { return CmsTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return CmsTemplateRow.localTextPrefix; }
        protected getNameProperty() { return CmsTemplateRow.nameProperty; }
        protected getService() { return CmsTemplateService.baseUrl; }

        protected form = new CmsTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}