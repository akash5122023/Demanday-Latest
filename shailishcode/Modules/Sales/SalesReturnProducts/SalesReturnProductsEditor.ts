
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesReturnProductsEditor extends Common.GridEditorBase<SalesReturnProductsRow> {
        protected getColumnsKey() { return 'Sales.SalesReturnProducts'; }
        protected getDialogType() { return SalesReturnProductsEditorDialog; }
        protected getLocalTextPrefix() { return SalesReturnProductsRow.localTextPrefix; }

        protected validateEntity(row: SalesProductsRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;

            return true;
        }

        constructor(container: JQuery) {
            super(container);
        }
        protected getColumns(): Slick.Column[] {
            let columns = super.getColumns();

            // Get the Travels setting from your CompanyDetails lookup
            let travelsEnabled = Q.getLookup('Administration.CompanyDetails').itemById[1]?.Travels;

            if (travelsEnabled === false) {
                Q.first(columns, x => x.field === "From").visible = false;
                Q.first(columns, x => x.field === "To").visible = false;
                Q.first(columns, x => x.field === "Date").visible = false;
                Q.first(columns, x => x.field === "Destination").visible = false;
                Q.first(columns, x => x.field === "Nights").visible = false;
                Q.first(columns, x => x.field === "Adults").visible = false;
                Q.first(columns, x => x.field === "Childrens").visible = false;
                Q.first(columns, x => x.field === "HotelName").visible = false;
                Q.first(columns, x => x.field === "MealPlan").visible = false;
            }

            return columns;
        }
    }
}