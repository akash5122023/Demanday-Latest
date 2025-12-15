
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesFollowupsGrid extends Serenity.EntityGrid<SalesFollowupsRow, any> {
        protected getColumnsKey() { return 'Sales.SalesFollowups'; }
        protected getIdProperty() { return SalesFollowupsRow.idProperty; }
        protected getInsertPermission() { return SalesFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return SalesFollowupsRow.localTextPrefix; }
        protected getService() { return SalesFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ SalesId: this.SalesId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.SalesId;
        }

        private _SalesId: string;

        get SalesId() {
            return this._SalesId;
        }

        set SalesId(value: string) {
            if (this._SalesId !== value) {
                this._SalesId = value;
                this.setEquality('SalesId', value);
                this.refresh();
            }
        }
    }
}