
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class AMCProductsEditor extends Common.GridEditorBase<AMCProductsRow> {
        protected getColumnsKey() { return 'Services.AMCProducts'; }
        protected getDialogType() { return AMCProductsEditorDialog; }
        protected getLocalTextPrefix() { return AMCProductsRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }

        validateEntity(row: AMCProductsRow, id) {
            row.ProductsId = Q.toId(row.ProductsId);

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;
            //row.LineTotal = (row.Quantity || 0) * (row.Rate || 0) - (row.Discount || 0);
            return true;

        }

    }
}