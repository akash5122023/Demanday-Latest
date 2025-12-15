
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class TradeIndiaConfigurationDialog extends ConfigurationDialog<TradeIndiaConfigurationRow> {
        protected getFormKey() { return TradeIndiaConfigurationForm.formKey; }
        protected getIdProperty() { return TradeIndiaConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return TradeIndiaConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return TradeIndiaConfigurationRow.nameProperty; }
        protected getService() { return TradeIndiaConfigurationService.baseUrl; }
        protected getDeletePermission() { return TradeIndiaConfigurationRow.deletePermission; }
        protected getInsertPermission() { return TradeIndiaConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return TradeIndiaConfigurationRow.updatePermission; }

        protected form = new TradeIndiaConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}