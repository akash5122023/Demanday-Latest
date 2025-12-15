namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class DefaultFeatureGrid extends GridBase<DefaultFeatureRow, any> {
        protected getColumnsKey() { return 'Settings.DefaultFeature'; }
        protected getDialogType() { return DefaultFeatureDialog; }
        protected getIdProperty() { return DefaultFeatureRow.idProperty; }
        protected getInsertPermission() { return DefaultFeatureRow.insertPermission; }
        protected getLocalTextPrefix() { return DefaultFeatureRow.localTextPrefix; }
        protected getService() { return 'Settings/DefaultFeatures'; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}
