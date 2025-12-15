
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceAppointmentsGrid extends Serenity.EntityGrid<InvoiceAppointmentsRow, any> {
        protected getColumnsKey() { return 'Sales.InvoiceAppointments'; }
        protected getIdProperty() { return InvoiceAppointmentsRow.idProperty; }
        protected getInsertPermission() { return InvoiceAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return InvoiceAppointmentsRow.localTextPrefix; }
        protected getService() { return InvoiceAppointmentsService.baseUrl; }

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