/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class IndentProductsEditor extends Common.GridEditorBase<IndentProductsRow> {
        protected getColumnsKey() { return 'Sales.IndentProducts'; }
        protected getDialogType() { return IndentProductsEditorDialog; }
        protected getLocalTextPrefix() { return IndentProductsRow.localTextPrefix; }
        
        public ContactsId: number;
        constructor(container: JQuery) {
            super(container);
        }
        validateEntity(row: IndentProductsRow, id) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsId = Q.toId(row.ProductsId);

            var sameProduct = Q.tryFirst(this.view.getItems(), x => x.ProductsId === row.ProductsId);
            if (sameProduct && this.id(sameProduct) !== id) {
                Q.alert('This product is already added!');
                return false;
            }

            // Set ProductsName based on selected product
            const product = Products.ProductsRow.getLookup().itemById[row.ProductsId];
            row.ProductsName = product?.Name || '';

            return true;
        }
    }
}