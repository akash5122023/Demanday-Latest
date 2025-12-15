
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class CMSProductsEditor extends Common.GridEditorBase<CMSProductsRow> {
        protected getColumnsKey() { return 'Services.CMSProducts'; }
        protected getDialogType() { return CMSProductsEditorDialog; }
        protected getLocalTextPrefix() { return CMSProductsRow.localTextPrefix; }

        protected validateEntity(row: CMSProductsRow, id: number) {
            if (!super.validateEntity(row, id))
                return false;

            row.ProductsName = Products.ProductsRow.getLookup().itemById[row.ProductsId].Name;

            return true;
        }

        constructor(container: JQuery) {
            super(container);
        }
    }
}