
namespace AdvanceCRM.Employee {
    import fld = EmployeeRow.Fields;

    @Serenity.Decorators.registerClass()
    export class EmployeeGrid extends GridBase<EmployeeRow, any> {
        protected getColumnsKey() { return 'Employee.Employee'; }
        protected getDialogType() { return EmployeeDialog; }
        protected getIdProperty() { return EmployeeRow.idProperty; }
        protected getInsertPermission() { return EmployeeRow.insertPermission; }
        protected getLocalTextPrefix() { return EmployeeRow.localTextPrefix; }
        protected getService() { return EmployeeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }


    }
}