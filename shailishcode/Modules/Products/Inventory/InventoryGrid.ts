
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class InventoryGrid extends Serenity.EntityGrid<InventoryRow, any> {
        private static readonly idProperty = 'Id';
        private static readonly localTextPrefix = 'Inventory.Inventory';
        private static readonly insertPermission = 'Inventory:Insert';

        protected getColumnsKey() { return InventoryColumns.columnsKey; }
        protected getDialogType() { return InventoryDialog; }
        protected getIdProperty() { return InventoryGrid.idProperty; }
        protected getInsertPermission() { return InventoryGrid.insertPermission; }
        protected getLocalTextPrefix() { return InventoryGrid.localTextPrefix; }
        protected getService() { return InventoryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            var filterButton = buttons.pop();

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
                }
            });

            buttons.push({
                title: 'Stock',
                cssClass: 'export-pdf-button',
                onClick: () => {
                    _Ext.ReportHelper.execute({ reportKey: 'Reports.InventoryReport', params: { Request: { Type: '1' } }, extension: 'pdf' });
                }
            });

            buttons.push(filterButton);

            return buttons;
        }

        protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            Common.NavigationService.ChannelsManagement({},
                response => {
                    if (response.Status == "Remove") {
                        columns.splice(Q.indexOf(columns, x => x.field == "ChannelCustomerPrice"), 7);
                        return columns;
                    }
                }
            );

            return columns;
        }
        protected createQuickFilters() {
            super.createQuickFilters();

            this.addQuickFilter({
                type: Serenity.BooleanEditor,
                field: "RawMaterial",
                title: "Raw Material"
            }).value = false;
        }
    }
}