
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class PurchaseOrderTemplateDialog extends ConfigurationDialog<PurchaseOrderTemplateRow> {
        protected getFormKey() { return PurchaseOrderTemplateForm.formKey; }
        protected getIdProperty() { return PurchaseOrderTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return PurchaseOrderTemplateRow.localTextPrefix; }
        protected getNameProperty() { return PurchaseOrderTemplateRow.nameProperty; }
        protected getService() { return PurchaseOrderTemplateService.baseUrl; }

        protected form = new PurchaseOrderTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}