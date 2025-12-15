
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceFollowupsGrid extends Serenity.EntityGrid<InvoiceFollowupsRow, any> {
        protected getColumnsKey() { return 'Sales.InvoiceFollowups'; }
        protected getIdProperty() { return InvoiceFollowupsRow.idProperty; }
        protected getInsertPermission() { return InvoiceFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return InvoiceFollowupsRow.localTextPrefix; }
        protected getService() { return InvoiceFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ InvoiceId: this.InvoiceId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.InvoiceId;
        }

        private _InvoiceId: string;

        get InvoiceId() {
            return this._InvoiceId;
        }

        set InvoiceId(value: string) {
            if (this._InvoiceId !== value) {
                this._InvoiceId = value;
                this.setEquality('InvoiceId', value);
                this.refresh();
            }
        }
    }
}