
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class AiConfigurationGrid extends Serenity.EntityGrid<AiConfigurationRow, any> {
        protected getColumnsKey() { return AiConfigurationColumns.columnsKey; }
        protected getDialogType() { return AiConfigurationDialog; }
        protected getIdProperty() { return AiConfigurationRow.idProperty; }
        protected getInsertPermission() { return AiConfigurationRow.insertPermission; }
        protected getLocalTextPrefix() { return AiConfigurationRow.localTextPrefix; }
        protected getService() { return AiConfigurationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}