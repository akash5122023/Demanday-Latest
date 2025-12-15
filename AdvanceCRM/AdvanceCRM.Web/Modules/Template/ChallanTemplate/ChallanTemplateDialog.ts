
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class ChallanTemplateDialog extends ConfigurationDialog<ChallanTemplateRow> {
        protected getFormKey() { return ChallanTemplateForm.formKey; }
        protected getIdProperty() { return ChallanTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return ChallanTemplateRow.localTextPrefix; }
        protected getNameProperty() { return ChallanTemplateRow.nameProperty; }
        protected getService() { return ChallanTemplateService.baseUrl; }

        protected form = new ChallanTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}