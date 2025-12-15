

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProductsGroupGrid extends GridBase<ProductsGroupRow, any> {
        protected getColumnsKey() { return 'Masters.ProductsGroup'; }
        protected getDialogType() { return ProductsGroupDialog; }
        protected getIdProperty() { return ProductsGroupRow.idProperty; }
        protected getInsertPermission() { return ProductsGroupRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductsGroupRow.localTextPrefix; }
        protected getService() { return ProductsGroupService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}