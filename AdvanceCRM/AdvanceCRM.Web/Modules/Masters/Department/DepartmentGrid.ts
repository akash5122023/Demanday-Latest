
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DepartmentGrid extends GridBase<DepartmentRow, any> {
        protected getColumnsKey() { return 'Masters.Department'; }
        protected getDialogType() { return DepartmentDialog; }
        protected getIdProperty() { return DepartmentRow.idProperty; }
        protected getInsertPermission() { return DepartmentRow.insertPermission; }
        protected getLocalTextPrefix() { return DepartmentRow.localTextPrefix; }
        protected getService() { return DepartmentService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}