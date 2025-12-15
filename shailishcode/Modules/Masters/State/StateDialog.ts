
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class StateDialog extends DialogBase<StateRow, any> {
        protected getFormKey() { return StateForm.formKey; }
        protected getIdProperty() { return StateRow.idProperty; }
        protected getLocalTextPrefix() { return StateRow.localTextPrefix; }
        protected getNameProperty() { return StateRow.nameProperty; }
        protected getService() { return StateService.baseUrl; }
        protected getDeletePermission() { return StateRow.deletePermission; }
        protected getInsertPermission() { return StateRow.insertPermission; }
        protected getUpdatePermission() { return StateRow.updatePermission; }

        protected form = new StateForm(this.idPrefix);

    }
}