

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsUnitGrid extends GridBase<ProductsUnitRow, any> {
        protected getColumnsKey() { return 'Masters.ProductsUnit'; }
        protected getDialogType() { return ProductsUnitDialog; }
        protected getIdProperty() { return ProductsUnitRow.idProperty; }
        protected getInsertPermission() { return ProductsUnitRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductsUnitRow.localTextPrefix; }
        protected getService() { return ProductsUnitService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}