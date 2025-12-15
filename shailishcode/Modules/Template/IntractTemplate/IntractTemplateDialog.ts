
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class IntractTemplateDialog extends Serenity.EntityDialog<IntractTemplateRow, any> {
        protected getFormKey() { return IntractTemplateForm.formKey; }
        protected getIdProperty() { return IntractTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return IntractTemplateRow.localTextPrefix; }
        protected getNameProperty() { return IntractTemplateRow.nameProperty; }
        protected getService() { return IntractTemplateService.baseUrl; }
        protected getDeletePermission() { return IntractTemplateRow.deletePermission; }
        protected getInsertPermission() { return IntractTemplateRow.insertPermission; }
        protected getUpdatePermission() { return IntractTemplateRow.updatePermission; }

        protected form = new IntractTemplateForm(this.idPrefix);

    }
}