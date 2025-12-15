
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaskGrid extends Serenity.EntityGrid<TaskRow, any> {
        protected getColumnsKey() { return 'Masters.Task'; }
        protected getDialogType() { return TaskDialog; }
        protected getIdProperty() { return TaskRow.idProperty; }
        protected getInsertPermission() { return TaskRow.insertPermission; }
        protected getLocalTextPrefix() { return TaskRow.localTextPrefix; }
        protected getService() { return TaskService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}