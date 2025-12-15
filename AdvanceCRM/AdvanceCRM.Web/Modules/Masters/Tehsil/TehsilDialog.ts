
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TehsilDialog extends DialogBase<TehsilRow, any> {
        protected getFormKey() { return TehsilForm.formKey; }
        protected getIdProperty() { return TehsilRow.idProperty; }
        protected getLocalTextPrefix() { return TehsilRow.localTextPrefix; }
        protected getNameProperty() { return TehsilRow.nameProperty; }
        protected getService() { return TehsilService.baseUrl; }
        protected getDeletePermission() { return TehsilRow.deletePermission; }
        protected getInsertPermission() { return TehsilRow.insertPermission; }
        protected getUpdatePermission() { return TehsilRow.updatePermission; }

        protected form = new TehsilForm(this.idPrefix);

    }
}