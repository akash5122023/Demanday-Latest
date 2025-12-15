/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    //@Serenity.Decorators.maximizable()

    export class GrnProductsTwoEditor extends Common.GridEditorBase<GrnProductsTwoRow> {
        protected getColumnsKey() { return 'Purchase.GrnProductsTwo'; }
        protected getDialogType() { return GrnProductsTwoEditorDialog; }
        protected getLocalTextPrefix() { return GrnProductsTwoRow.localTextPrefix; }

        public ContactsId: number;

        constructor(container: JQuery) {
            super(container);
        }

        protected validateEntity(row: GrnProductsTwoRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsId = Q.toId(row.ProductsId);

            var sameProduct = Q.tryFirst(this.view.getItems(), x => x.ProductsId === row.ProductsId);
            if (sameProduct && this.id(sameProduct) !== id) {
                Q.alert('This product is already in cart!');
                return false;
            }

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;

            // Set ProductsName based on selected product
            //const product = Products.InventoryRow.getLookup().itemById[row.ProductsId];
            //row.ProductsName = product?.Name || '';



            return true;
        }

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);

            // passing Contacts ID from grid editor to detail dialog
            (dialog as GrnProductsTwoEditorDialog).ContactsIdD = this.ContactsId;
        }

    }
}
