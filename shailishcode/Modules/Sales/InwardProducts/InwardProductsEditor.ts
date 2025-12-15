/// <reference path="../../Common/Helpers/GridEditorBase.ts" />
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InwardProductsEditor extends Common.GridEditorBase<InwardProductsRow> {
        protected getColumnsKey() { return 'Sales.InwardProducts'; }
        protected getDialogType() { return InwardProductsEditorDialog; }
        protected getLocalTextPrefix() { return InwardProductsRow.localTextPrefix; }

        public ContactsId: number;

        protected validateEntity(row: InwardProductsRow, id: number) {
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
            (dialog as InwardProductsEditorDialog).ContactsIdD = this.ContactsId;
        }
    }
}