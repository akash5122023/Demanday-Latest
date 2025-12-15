

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsDivisionGrid extends GridBase<ProductsDivisionRow, any> {
        protected getColumnsKey() { return 'Masters.ProductsDivision'; }
        protected getDialogType() { return ProductsDivisionDialog; }
        protected getIdProperty() { return ProductsDivisionRow.idProperty; }
        protected getInsertPermission() { return ProductsDivisionRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductsDivisionRow.localTextPrefix; }
        protected getService() { return ProductsDivisionService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}