

namespace AdvanceCRM.Masters {

    import fld = TehsilRow.Fields;
    @Serenity.Decorators.registerClass()
    export class TehsilGrid extends GridBase<TehsilRow, any> {
        protected getColumnsKey() { return 'Masters.Tehsil'; }
        protected getDialogType() { return TehsilDialog; }
        protected getIdProperty() { return TehsilRow.idProperty; }
        protected getInsertPermission() { return TehsilRow.insertPermission; }
        protected getLocalTextPrefix() { return TehsilRow.localTextPrefix; }
        protected getService() { return TehsilService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            this.view.setGrouping(
                [{
                    formatter: x => 'State: ' + x.value + ' (' + x.count + ' items)',
                    getter: fld.State
                }, {
                    formatter: x => 'City: ' + x.value + ' (' + x.count + ' items)',
                    getter: fld.City
                }])
        }

        protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            Q.first(columns, x => x.field == fld.State).format =
                ctx => `<a href="javascript:;" class="state-link">${Q.htmlEncode(ctx.value)}</a>`;

            Q.first(columns, x => x.field == fld.City).format =
                ctx => `<a href="javascript:;" class="city-link">${Q.formatDate(ctx.value)}</a>`;

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
            else if (target.hasClass("city-link")) {
                e.preventDefault();

                var c = Q.first(CityRow.getLookup().items,
                    x => x.Id == item.CityId);

                new CityDialog().loadByIdAndOpenDialog(c.Id);
            }
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.push(AdvanceCRM.Common.ExcelExportHelper.createToolButton({
                grid: this,
                onViewSubmit: () => this.onViewSubmit(),
                service: 'Masters/Tehsil/ListExcel',
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
                    var dialog = new TehsilExcelImportDialog();
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