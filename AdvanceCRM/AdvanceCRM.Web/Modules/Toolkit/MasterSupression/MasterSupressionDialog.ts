
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class MasterSupressionDialog extends DialogBase<MasterSupressionRow, any> {
        protected getFormKey() { return MasterSupressionForm.formKey; }
        protected getIdProperty() { return MasterSupressionRow.idProperty; }
        protected getLocalTextPrefix() { return MasterSupressionRow.localTextPrefix; }
        protected getNameProperty() { return MasterSupressionRow.nameProperty; }
        protected getService() { return MasterSupressionService.baseUrl; }
        protected getDeletePermission() { return MasterSupressionRow.deletePermission; }
        protected getInsertPermission() { return MasterSupressionRow.insertPermission; }
        protected getUpdatePermission() { return MasterSupressionRow.updatePermission; }

        protected form = new MasterSupressionForm(this.idPrefix);

    }
}