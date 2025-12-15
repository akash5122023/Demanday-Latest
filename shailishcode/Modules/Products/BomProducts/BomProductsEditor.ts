
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class BomProductsEditor extends Common.GridEditorBase<BomProductsRow> {
        protected getColumnsKey() { return 'Products.BomProducts'; }
        protected getDialogType() { return BomProductsEditorDialog; }
        protected getLocalTextPrefix() { return BomProductsRow.localTextPrefix; }

        public ContactsId: number;

        constructor(container: JQuery) {
            super(container);
        }
        validateEntity(row: BomProductsRow, id) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsId = Q.toId(row.ProductsId);

            var sameProduct = Q.tryFirst(this.view.getItems(), x => x.ProductsId === row.ProductsId);
            if (sameProduct && this.id(sameProduct) !== id) {
                Q.alert('This product is already in cart!');
                return false;
            }

            //row.ProductsName = Products.InventoryRow.getLookup().itemById[row.ProductsId].Name;
            //row.LineTotal = (row.Quantity || 0) * (row.Price || 0) - (row.Discount || 0);
            return true;
        }

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);

            // passing Contacts ID from grid editor to detail dialog
            (dialog as BomProductsEditorDialog).ContactsIdD = this.ContactsId;
        }
    }
}