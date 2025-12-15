

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskStatusGrid extends GridBase<TaskStatusRow, any> {
        protected getColumnsKey() { return 'Masters.TaskStatus'; }
        protected getDialogType() { return TaskStatusDialog; }
        protected getIdProperty() { return TaskStatusRow.idProperty; }
        protected getInsertPermission() { return TaskStatusRow.insertPermission; }
        protected getLocalTextPrefix() { return TaskStatusRow.localTextPrefix; }
        protected getService() { return TaskStatusService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}