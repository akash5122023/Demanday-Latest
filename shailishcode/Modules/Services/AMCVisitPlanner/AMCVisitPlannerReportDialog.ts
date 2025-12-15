
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class AMCVisitPlannerReportDialog extends ReadOnlyDialog<AMCVisitPlannerRow> {
        protected getFormKey() { return AMCVisitPlannerForm.formKey; }
        protected getIdProperty() { return AMCVisitPlannerRow.idProperty; }
        protected getLocalTextPrefix() { return AMCVisitPlannerRow.localTextPrefix; }
        protected getNameProperty() { return AMCVisitPlannerRow.nameProperty; }
        protected getService() { return AMCVisitPlannerService.baseUrl; }
        protected getDeletePermission() { return AMCVisitPlannerRow.deletePermission; }
        protected getInsertPermission() { return AMCVisitPlannerRow.insertPermission; }
        protected getUpdatePermission() { return AMCVisitPlannerRow.updatePermission; }

        protected form = new AMCVisitPlannerForm(this.idPrefix);

    }
}