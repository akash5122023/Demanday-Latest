
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class AppointmentTemplateDialog extends ConfigurationDialog<AppointmentTemplateRow> {
        protected getFormKey() { return AppointmentTemplateForm.formKey; }
        protected getIdProperty() { return AppointmentTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return AppointmentTemplateRow.localTextPrefix; }
        protected getNameProperty() { return AppointmentTemplateRow.nameProperty; }
        protected getService() { return AppointmentTemplateService.baseUrl; }

        protected form = new AppointmentTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}