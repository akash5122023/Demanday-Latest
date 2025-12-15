
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class AMCVisitPlannerGrid extends Serenity.EntityGrid<AMCVisitPlannerRow, any> {
        protected getColumnsKey() { return 'Services.AMCVisitPlanner'; }
        protected getIdProperty() { return AMCVisitPlannerRow.idProperty; }
        protected getInsertPermission() { return AMCVisitPlannerRow.insertPermission; }
        protected getLocalTextPrefix() { return AMCVisitPlannerRow.localTextPrefix; }
        protected getService() { return AMCVisitPlannerService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ AMCId: this.AMCId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.AMCId;
        }

        private _AMCId: string;

        get AMCId() {
            return this._AMCId;
        }

        set AMCId(value: string) {
            if (this._AMCId !== value) {
                this._AMCId = value;
                this.setEquality('AMCId', value);
                this.refresh();
            }
        }
    }
}