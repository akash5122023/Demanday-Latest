
namespace AdvanceCRM.Template {
    @Serenity.Decorators.panel()
    @Serenity.Decorators.registerClass()
    export class OtherTemplatesDialog extends DialogBase<OtherTemplatesRow, any> {
        protected getFormKey() { return OtherTemplatesForm.formKey; }
        protected getIdProperty() { return OtherTemplatesRow.idProperty; }
        protected getLocalTextPrefix() { return OtherTemplatesRow.localTextPrefix; }
        protected getNameProperty() { return OtherTemplatesRow.nameProperty; }
        protected getService() { return OtherTemplatesService.baseUrl; }
        protected getDeletePermission() { return OtherTemplatesRow.deletePermission; }
        protected getInsertPermission() { return OtherTemplatesRow.insertPermission; }
        protected getUpdatePermission() { return OtherTemplatesRow.updatePermission; }

        protected form = new OtherTemplatesForm(this.idPrefix);

    }
}