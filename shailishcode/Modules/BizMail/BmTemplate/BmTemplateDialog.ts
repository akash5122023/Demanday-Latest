
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmTemplateDialog extends DialogBase<BmTemplateRow, any> {
        protected getFormKey() { return BmTemplateForm.formKey; }
        protected getIdProperty() { return BmTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return BmTemplateRow.localTextPrefix; }
        protected getNameProperty() { return BmTemplateRow.nameProperty; }
        protected getService() { return BmTemplateService.baseUrl; }
        protected getDeletePermission() { return BmTemplateRow.deletePermission; }
        protected getInsertPermission() { return BmTemplateRow.insertPermission; }
        protected getUpdatePermission() { return BmTemplateRow.updatePermission; }

        protected form = new BmTemplateForm(this.idPrefix);

    }
}