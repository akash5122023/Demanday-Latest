
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class ProductsGrid extends GridBase<ProductsRow, any> {
        protected getColumnsKey() { return 'Products.Products'; }
        protected getDialogType() { return ProductsDialog; }
        protected getIdProperty() { return Products.ProductsRow.idProperty; }
        protected getInsertPermission() { return ProductsRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductsRow.localTextPrefix; }
        protected getService() { return ProductsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        getButtons() {
            var buttons = super.getButtons();
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
    }
}