
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DaysDialog extends Serenity.EntityDialog<DaysRow, any> {
        protected getFormKey() { return DaysForm.formKey; }
        protected getIdProperty() { return DaysRow.idProperty; }
        protected getLocalTextPrefix() { return DaysRow.localTextPrefix; }
        protected getNameProperty() { return DaysRow.nameProperty; }
        protected getService() { return DaysService.baseUrl; }
        protected getDeletePermission() { return DaysRow.deletePermission; }
        protected getInsertPermission() { return DaysRow.insertPermission; }
        protected getUpdatePermission() { return DaysRow.updatePermission; }

        protected form = new DaysForm(this.idPrefix);

    }
}