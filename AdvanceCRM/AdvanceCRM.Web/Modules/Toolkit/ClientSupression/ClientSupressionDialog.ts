
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class ClientSupressionDialog extends DialogBase<ClientSupressionRow, any> {
        protected getFormKey() { return ClientSupressionForm.formKey; }
        protected getIdProperty() { return ClientSupressionRow.idProperty; }
        protected getLocalTextPrefix() { return ClientSupressionRow.localTextPrefix; }
        protected getNameProperty() { return ClientSupressionRow.nameProperty; }
        protected getService() { return ClientSupressionService.baseUrl; }
        protected getDeletePermission() { return ClientSupressionRow.deletePermission; }
        protected getInsertPermission() { return ClientSupressionRow.insertPermission; }
        protected getUpdatePermission() { return ClientSupressionRow.updatePermission; }

        protected form = new ClientSupressionForm(this.idPrefix);

    }
}