

namespace AdvanceCRM.Masters {

    import fld = CityRow.Fields;
    @Serenity.Decorators.registerClass()
    export class CityGrid extends GridBase<CityRow, any> {
        protected getColumnsKey() { return 'Masters.City'; }
        protected getDialogType() { return CityDialog; }
        protected getIdProperty() { return CityRow.idProperty; }
        protected getInsertPermission() { return CityRow.insertPermission; }
        protected getLocalTextPrefix() { return CityRow.localTextPrefix; }
        protected getService() { return CityService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            this.view.setGrouping(
                [{
                    formatter: x => 'State: ' + x.value + ' (' + x.count + ' items)',
                    getter: fld.State
                }])
        }

        protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            Q.first(columns, x => x.field == fld.State).format =
                ctx => `<a href="javascript:;" class="state-link">${Q.htmlEncode(ctx.value)}</a>`;

            return columns;
        }

        protected onClick(e: JQueryEventObject, row: number, cell: number): void {

            // let base grid handle clicks for its edit links
            super.onClick(e, row, cell);

            // if base grid already handled, we shouldn"t handle it again
            if (e.isDefaultPrevented()) {
                return;
            }

            // get reference to current item
            var item = this.itemAt(row);

            // get reference to clicked element
            var target = $(e.target);

            if (target.hasClass("state-link")) {
                e.preventDefault();

                var s = Q.first(StateRow.getLookup().items,
                    x => x.Id == item.StateId);

                new StateDialog().loadByIdAndOpenDialog(s.Id);
            }
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.push(AdvanceCRM.Common.ExcelExportHelper.createToolButton({
                grid: this,
                onViewSubmit: () => this.onViewSubmit(),
                service: 'Masters/City/ListExcel',
                separator: true
            }));

            buttons.push(_Ext.PdfExportHelper.createToolButton({
                grid: this,
                onViewSubmit: () => this.onViewSubmit()
            }));

            buttons.push({
                title: 'Import',
                cssClass: 'export-xlsx-button',
                onClick: () => {
                    // open import dialog, let it handle rest
                    var dialog = new CityExcelImportDialog();
                    dialog.element.on('dialogclose', () => {
                        this.refresh();
                        dialog = null;
                    });
                    dialog.dialogOpen();
                }
            });

            return buttons;
        }
    }
}