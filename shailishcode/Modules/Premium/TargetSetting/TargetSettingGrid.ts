
namespace AdvanceCRM.Premium {

    @Serenity.Decorators.registerClass()
    export class TargetSettingGrid extends Serenity.EntityGrid<TargetSettingRow, any> {
        protected getColumnsKey() { return 'Premium.TargetSetting'; }
        protected getDialogType() { return TargetSettingDialog; }
        protected getIdProperty() { return TargetSettingRow.idProperty; }
        protected getInsertPermission() { return TargetSettingRow.insertPermission; }
        protected getLocalTextPrefix() { return TargetSettingRow.localTextPrefix; }
        protected getService() { return TargetSettingService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}