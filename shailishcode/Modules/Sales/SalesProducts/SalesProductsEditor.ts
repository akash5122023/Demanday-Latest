
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesProductsEditor extends Common.GridEditorBase<SalesProductsRow> {
        protected getColumnsKey() { return 'Sales.SalesProducts'; }
        protected getDialogType() { return SalesProductsEditorDialog; }
        protected getLocalTextPrefix() { return SalesProductsRow.localTextPrefix; }

        public ContactsId: number;

        protected validateEntity(row: SalesProductsRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsId = Q.toId(row.ProductsId);

            var sameProduct = Q.tryFirst(this.view.getItems(), x => x.ProductsId === row.ProductsId);
            if (sameProduct && this.id(sameProduct) !== id) {
                Q.alert('This product is already in cart!');
                return false;
            }

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;
            row.LineTotal = (row.Quantity || 0) * (row.Price || 0) - (row.Discount || 0);
            return true;
        }

        constructor(container: JQuery) {
            super(container);
        }

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);

            // passing Contacts ID from grid editor to detail dialog
            (dialog as SalesProductsEditorDialog).ContactsIdD = this.ContactsId;
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