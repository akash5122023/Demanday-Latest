
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class InstamojoDialog extends Serenity.EntityDialog<InstamojoRow, any> {
        protected getFormKey() { return InstamojoForm.formKey; }
        protected getIdProperty() { return InstamojoRow.idProperty; }
        protected getLocalTextPrefix() { return InstamojoRow.localTextPrefix; }
        protected getNameProperty() { return InstamojoRow.nameProperty; }
        protected getService() { return InstamojoService.baseUrl; }
        protected getDeletePermission() { return InstamojoRow.deletePermission; }
        protected getInsertPermission() { return InstamojoRow.insertPermission; }
        protected getUpdatePermission() { return InstamojoRow.updatePermission; }

        protected form = new InstamojoForm(this.idPrefix);

    }
}