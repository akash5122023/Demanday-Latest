
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationFollowupsGrid extends Serenity.EntityGrid<QuotationFollowupsRow, any> {
        protected getColumnsKey() { return 'Quotation.QuotationFollowups'; }
        protected getIdProperty() { return QuotationFollowupsRow.idProperty; }
        protected getInsertPermission() { return QuotationFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return QuotationFollowupsRow.localTextPrefix; }
        protected getService() { return QuotationFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ QuotationId: this.QuotationId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.QuotationId;
        }

        private _QuotationId: string;

        get QuotationId() {
            return this._QuotationId;
        }

        set QuotationId(value: string) {
            if (this._QuotationId !== value) {
                this._QuotationId = value;
                this.setEquality('QuotationId', value);
                this.refresh();
            }
        }
    }
}