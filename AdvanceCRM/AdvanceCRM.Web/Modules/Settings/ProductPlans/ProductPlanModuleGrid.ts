namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductPlanModuleGrid extends GridBase<ProductPlanModuleRow, any> {
        protected getColumnsKey() { return 'Settings.ProductPlanModule'; }
        protected getDialogType() { return ProductPlanModuleDialog; }
        protected getIdProperty() { return ProductPlanModuleRow.idProperty; }
        protected getInsertPermission() { return ProductPlanModuleRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductPlanModuleRow.localTextPrefix; }
        protected getService() { return ProductPlanModulesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}
