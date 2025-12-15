
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class AMCVisitPlannerReportGrid extends GridBase<AMCVisitPlannerRow, any> {
        protected getColumnsKey() { return 'Services.AMCVisitPlanner'; }
        protected getDialogType() { return AMCVisitPlannerReportDialog; }
        protected getIdProperty() { return AMCVisitPlannerRow.idProperty; }
        protected getInsertPermission() { return AMCVisitPlannerRow.insertPermission; }
        protected getLocalTextPrefix() { return AMCVisitPlannerRow.localTextPrefix; }
        protected getService() { return AMCVisitPlannerService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            return buttons;
        }
    }
}