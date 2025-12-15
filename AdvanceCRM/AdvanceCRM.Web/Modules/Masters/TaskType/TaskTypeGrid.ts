

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskTypeGrid extends GridBase<TaskTypeRow, any> {
        protected getColumnsKey() { return 'Masters.TaskType'; }
        protected getDialogType() { return TaskTypeDialog; }
        protected getIdProperty() { return TaskTypeRow.idProperty; }
        protected getInsertPermission() { return TaskTypeRow.insertPermission; }
        protected getLocalTextPrefix() { return TaskTypeRow.localTextPrefix; }
        protected getService() { return TaskTypeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}