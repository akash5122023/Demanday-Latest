namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ProductPlanGrid extends GridBase<ProductPlanRow, any> {
        protected getColumnsKey() { return 'Settings.ProductPlan'; }
        protected getDialogType() { return ProductPlanDialog; }
        protected getIdProperty() { return ProductPlanRow.idProperty; }
        protected getInsertPermission() { return ProductPlanRow.insertPermission; }
        protected getLocalTextPrefix() { return ProductPlanRow.localTextPrefix; }
        protected getService() { return 'Settings/ProductPlans'; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}
