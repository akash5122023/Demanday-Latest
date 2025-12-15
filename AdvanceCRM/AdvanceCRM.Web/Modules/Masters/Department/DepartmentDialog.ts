
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DepartmentDialog extends DialogBase<DepartmentRow, any> {
        protected getFormKey() { return DepartmentForm.formKey; }
        protected getIdProperty() { return DepartmentRow.idProperty; }
        protected getLocalTextPrefix() { return DepartmentRow.localTextPrefix; }
        protected getNameProperty() { return DepartmentRow.nameProperty; }
        protected getService() { return DepartmentService.baseUrl; }
        protected getDeletePermission() { return DepartmentRow.deletePermission; }
        protected getInsertPermission() { return DepartmentRow.insertPermission; }
        protected getUpdatePermission() { return DepartmentRow.updatePermission; }

        protected form = new DepartmentForm(this.idPrefix);

    }
}