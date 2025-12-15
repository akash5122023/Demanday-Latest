
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuotationProductsEditor extends Common.GridEditorBase<QuotationProductsRow> {
        protected getColumnsKey() { return 'Quotation.QuotationProducts'; }
        protected getDialogType() { return QuotationProductsEditorDialog; }
        protected getLocalTextPrefix() { return QuotationProductsRow.localTextPrefix; }

        public ContactsId: number;

        constructor(container: JQuery) {
            super(container);
        }

  

        validateEntity(row: QuotationProductsRow, id) {
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

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);

            // passing Contacts ID from grid editor to detail dialog
            (dialog as QuotationProductsEditorDialog).ContactsIdD = this.ContactsId;
        }

        protected getColumns(): Slick.Column[] {
            let columns = super.getColumns();

            // Load the CompanyDetails lookup
            let companyDetails = Q.getLookup('Administration.CompanyDetails');
            let travelsEnabled = companyDetails.items.length > 0 ? companyDetails.items[0].Travels : true;

            if (!travelsEnabled) {
                // Filter out columns related to travel
                columns = columns.filter(x => [
                    "From", "To", "Date", "Destination", "Nights", "Adults",
                    "Childrens", "HotelName", "MealPlan"
                ].indexOf(x.field) === -1);
            }

            return columns;
        }

    }
}