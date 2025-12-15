
namespace AdvanceCRM.Employee {

    @Serenity.Decorators.registerClass()
    export class EmployeeAssestsDialog extends Serenity.EntityDialog<EmployeeAssestsRow, any> {
        protected getFormKey() { return EmployeeAssestsForm.formKey; }
        protected getIdProperty() { return EmployeeAssestsRow.idProperty; }
        protected getLocalTextPrefix() { return EmployeeAssestsRow.localTextPrefix; }
        protected getNameProperty() { return EmployeeAssestsRow.nameProperty; }
        protected getService() { return EmployeeAssestsService.baseUrl; }
        protected getDeletePermission() { return EmployeeAssestsRow.deletePermission; }
        protected getInsertPermission() { return EmployeeAssestsRow.insertPermission; }
        protected getUpdatePermission() { return EmployeeAssestsRow.updatePermission; }

        protected form = new EmployeeAssestsForm(this.idPrefix);


    }
}