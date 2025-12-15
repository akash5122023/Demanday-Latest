
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AreaDialog extends DialogBase<AreaRow, any> {
        protected getFormKey() { return AreaForm.formKey; }
        protected getIdProperty() { return AreaRow.idProperty; }
        protected getLocalTextPrefix() { return AreaRow.localTextPrefix; }
        protected getNameProperty() { return AreaRow.nameProperty; }
        protected getService() { return AreaService.baseUrl; }
        protected getDeletePermission() { return AreaRow.deletePermission; }
        protected getInsertPermission() { return AreaRow.insertPermission; }
        protected getUpdatePermission() { return AreaRow.updatePermission; }

        protected form = new AreaForm(this.idPrefix);

    }
}