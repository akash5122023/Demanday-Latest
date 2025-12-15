
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskDialog extends Serenity.EntityDialog<TaskRow, any> {
        protected getFormKey() { return TaskForm.formKey; }
        protected getIdProperty() { return TaskRow.idProperty; }
        protected getLocalTextPrefix() { return TaskRow.localTextPrefix; }
        protected getNameProperty() { return TaskRow.nameProperty; }
        protected getService() { return TaskService.baseUrl; }
        protected getDeletePermission() { return TaskRow.deletePermission; }
        protected getInsertPermission() { return TaskRow.insertPermission; }
        protected getUpdatePermission() { return TaskRow.updatePermission; }

        protected form = new TaskForm(this.idPrefix);

    }
}