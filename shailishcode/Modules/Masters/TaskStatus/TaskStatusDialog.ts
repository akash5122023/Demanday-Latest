
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskStatusDialog extends DialogBase<TaskStatusRow, any> {
        protected getFormKey() { return TaskStatusForm.formKey; }
        protected getIdProperty() { return TaskStatusRow.idProperty; }
        protected getLocalTextPrefix() { return TaskStatusRow.localTextPrefix; }
        protected getNameProperty() { return TaskStatusRow.nameProperty; }
        protected getService() { return TaskStatusService.baseUrl; }
        protected getDeletePermission() { return TaskStatusRow.deletePermission; }
        protected getInsertPermission() { return TaskStatusRow.insertPermission; }
        protected getUpdatePermission() { return TaskStatusRow.updatePermission; }

        protected form = new TaskStatusForm(this.idPrefix);

    }
}