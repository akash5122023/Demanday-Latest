
namespace AdvanceCRM.Employee {

    @Serenity.Decorators.registerClass()
    export class EmployeeAssestsGrid extends Serenity.EntityGrid<EmployeeAssestsRow, any> {
        protected getColumnsKey() { return 'Employee.EmployeeAssests'; }
        protected getDialogType() { return EmployeeAssestsDialog; }
        protected getIdProperty() { return EmployeeAssestsRow.idProperty; }
        protected getInsertPermission() { return EmployeeAssestsRow.insertPermission; }
        protected getLocalTextPrefix() { return EmployeeAssestsRow.localTextPrefix; }
        protected getService() { return EmployeeAssestsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }
        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ EmployeeId: this.employeeId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.employeeId;
        }

        private _employeeId: string;

        get employeeId() {
            return this._employeeId;
        }

        set employeeId(value: string) {
            if (this._employeeId !== value) {
                this._employeeId = value;
                this.setEquality('EmployeeId', value);
                this.refresh();
            }
        }
    }
}