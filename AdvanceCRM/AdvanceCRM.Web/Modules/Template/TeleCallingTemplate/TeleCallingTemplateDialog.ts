
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class TeleCallingTemplateDialog extends ConfigurationDialog<TeleCallingTemplateRow> {
        protected getFormKey() { return TeleCallingTemplateForm.formKey; }
        protected getIdProperty() { return TeleCallingTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingTemplateRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingTemplateRow.nameProperty; }
        protected getService() { return TeleCallingTemplateService.baseUrl; }
        protected getUpdatePermission() { return TeleCallingTemplateRow.updatePermission; }

        protected form = new TeleCallingTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}