/// <reference path="../../Common/Helpers/GridEditorBase.ts" />
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class OutwardProductsEditor extends Common.GridEditorBase<OutwardProductsRow> {
        protected getColumnsKey() { return 'Sales.OutwardProducts'; }
        protected getDialogType() { return OutwardProductsEditorDialog; }
        protected getLocalTextPrefix() { return OutwardProductsRow.localTextPrefix; }

        public ContactsId: number;

        protected validateEntity(row: OutwardProductsRow, id: number) {
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
            (dialog as OutwardProductsEditorDialog).ContactsIdD = this.ContactsId;
        }
    }
}