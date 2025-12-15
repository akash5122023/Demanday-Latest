
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingAppointmentsGrid extends Serenity.EntityGrid<TeleCallingAppointmentsRow, any> {
        protected getColumnsKey() { return 'Services.TeleCallingAppointments'; }
        protected getIdProperty() { return TeleCallingAppointmentsRow.idProperty; }
        protected getInsertPermission() { return TeleCallingAppointmentsRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleCallingAppointmentsRow.localTextPrefix; }
        protected getService() { return TeleCallingAppointmentsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ TeleCallingId: this.TeleCallingId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.TeleCallingId;
        }

        private _TeleCallingId: string;

        get TeleCallingId() {
            return this._TeleCallingId;
        }

        set TeleCallingId(value: string) {
            if (this._TeleCallingId !== value) {
                this._TeleCallingId = value;
                this.setEquality('TeleCallingId', value);
                this.refresh();
            }
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.push({
                title: 'Import',
                cssClass: 'export-xlsx-button',
                onClick: () => {
                    // open import dialog, let it handle rest
                    var dialog = new Common.ExcelImportDialog(this.getService());
                    dialog.element.on('dialogclose', () => {
                        this.refresh();
                        dialog = null;
                    });
                    dialog.dialogOpen();
                },
                separator: true
            });

            return buttons;
        }
    }
}