
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class StockTransferProductsEditor extends Common.GridEditorBase<StockTransferProductsRow> {
        protected getColumnsKey() { return 'Products.StockTransferProducts'; }
        protected getDialogType() { return StockTransferProductsEditorDialog; }
        protected getLocalTextPrefix() { return StockTransferProductsRow.localTextPrefix; }

        protected validateEntity(row: StockTransferProductsRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;

            return true;
        }

        constructor(container: JQuery) {
            super(container);
        }
    }
}