
namespace AdvanceCRM.Employee {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class EmployeeDialog extends DialogBase<EmployeeRow, any> {
        protected getFormKey() { return EmployeeForm.formKey; }
        protected getIdProperty() { return EmployeeRow.idProperty; }
        protected getLocalTextPrefix() { return EmployeeRow.localTextPrefix; }
        protected getNameProperty() { return EmployeeRow.nameProperty; }
        protected getService() { return EmployeeService.baseUrl; }
        protected getDeletePermission() { return EmployeeRow.deletePermission; }
        protected getInsertPermission() { return EmployeeRow.insertPermission; }
        protected getUpdatePermission() { return EmployeeRow.updatePermission; }

        protected form = new EmployeeForm(this.idPrefix);

        private assestsGrid: EmployeeAssestsGrid;


        constructor() {
            super();

            this.assestsGrid = new EmployeeAssestsGrid(this.byId('AssestsGrid'));

        }

        loadEntity(entity: EmployeeRow) {
            super.loadEntity(entity);
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Assests', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.assestsGrid.employeeId = entity.Id.toString();
                this.assestsGrid.refresh();

            }
        }
        getCloningEntity() {
            var clone = super.getCloningEntity();
            return clone;
        }


    }
}