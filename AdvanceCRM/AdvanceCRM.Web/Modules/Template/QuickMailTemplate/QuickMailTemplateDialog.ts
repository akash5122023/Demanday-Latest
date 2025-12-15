
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class QuickMailTemplateDialog extends Serenity.EntityDialog<QuickMailTemplateRow, any> {
        protected getFormKey() { return QuickMailTemplateForm.formKey; }
        protected getIdProperty() { return QuickMailTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return QuickMailTemplateRow.localTextPrefix; }
        protected getNameProperty() { return QuickMailTemplateRow.nameProperty; }
        protected getService() { return QuickMailTemplateService.baseUrl; }
        protected getDeletePermission() { return QuickMailTemplateRow.deletePermission; }
        protected getInsertPermission() { return QuickMailTemplateRow.insertPermission; }
        protected getUpdatePermission() { return QuickMailTemplateRow.updatePermission; }

        protected form = new QuickMailTemplateForm(this.idPrefix);

    }
}