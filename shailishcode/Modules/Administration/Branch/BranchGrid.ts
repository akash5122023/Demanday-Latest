
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class BranchGrid extends Serenity.EntityGrid<BranchRow, any> {
        protected getColumnsKey() { return 'Administration.Branch'; }
        protected getIdProperty() { return BranchRow.idProperty; }
        protected getInsertPermission() { return BranchRow.insertPermission; }
        protected getLocalTextPrefix() { return BranchRow.localTextPrefix; }
        protected getService() { return BranchService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ CompanyId: this.companyId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.companyId;
        }

        private _companyId: string;

        get companyId() {
            return this._companyId;
        }

        set companyId(value: string) {
            if (this._companyId !== value) {
                this._companyId = value;
                this.setEquality('CompanyId', value);
                this.refresh();
            }
        }
    }
}