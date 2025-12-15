
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class AMCTemplateDialog extends ConfigurationDialog<AMCTemplateRow> {
        protected getFormKey() { return AMCTemplateForm.formKey; }
        protected getIdProperty() { return AMCTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return AMCTemplateRow.localTextPrefix; }
        protected getNameProperty() { return AMCTemplateRow.nameProperty; }
        protected getService() { return AMCTemplateService.baseUrl; }

        protected form = new AMCTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}