
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class ChallanProductsEditor extends Common.GridEditorBase<ChallanProductsRow> {
        protected getColumnsKey() { return 'Sales.ChallanProducts'; }
        protected getDialogType() { return ChallanProductsEditorDialog; }
        protected getLocalTextPrefix() { return ChallanProductsRow.localTextPrefix; }

        public ContactsId: number;

        protected validateEntity(row: ChallanProductsRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;

            return true;
        }

        constructor(container: JQuery) {
            super(container);
        }

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);

            // passing Contacts ID from grid editor to detail dialog
            (dialog as ChallanProductsEditorDialog).ContactsIdD = this.ContactsId;
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