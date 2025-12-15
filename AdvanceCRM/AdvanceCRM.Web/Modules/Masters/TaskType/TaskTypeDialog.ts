
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskTypeDialog extends DialogBase<TaskTypeRow, any> {
        protected getFormKey() { return TaskTypeForm.formKey; }
        protected getIdProperty() { return TaskTypeRow.idProperty; }
        protected getLocalTextPrefix() { return TaskTypeRow.localTextPrefix; }
        protected getNameProperty() { return TaskTypeRow.nameProperty; }
        protected getService() { return TaskTypeService.baseUrl; }
        protected getDeletePermission() { return TaskTypeRow.deletePermission; }
        protected getInsertPermission() { return TaskTypeRow.insertPermission; }
        protected getUpdatePermission() { return TaskTypeRow.updatePermission; }

        protected form = new TaskTypeForm(this.idPrefix);

    }
}