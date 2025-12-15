namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductModuleGrid extends Serenity.EntityGrid<ProductModuleRow, any> {
        protected getColumnsKey() { return 'Settings.ProductModule'; }
        protected getDialogType() { return ProductModuleDialog; }
        protected getIdProperty() { return ProductModuleRow.idProperty; }
        protected getInsertPermission() { return ProductModuleRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductModuleRow.localTextPrefix; }
        protected getService() { return ProductModulesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getDefaultSortBy() {
            return [
                ProductModuleRow.Fields.CreatedOn + ' DESC',
                ProductModuleRow.Fields.Id + ' DESC'
            ];
        }
    }
}
