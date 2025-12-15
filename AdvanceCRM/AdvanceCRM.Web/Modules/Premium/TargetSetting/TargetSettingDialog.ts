
namespace AdvanceCRM.Premium {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class TargetSettingDialog extends Serenity.EntityDialog<TargetSettingRow, any> {
        protected getFormKey() { return TargetSettingForm.formKey; }
        protected getIdProperty() { return TargetSettingRow.idProperty; }
        protected getLocalTextPrefix() { return TargetSettingRow.localTextPrefix; }
        protected getService() { return TargetSettingService.baseUrl; }
        protected getDeletePermission() { return TargetSettingRow.deletePermission; }
        protected getInsertPermission() { return TargetSettingRow.insertPermission; }
        protected getUpdatePermission() { return TargetSettingRow.updatePermission; }

        protected form = new TargetSettingForm(this.idPrefix);

    }
}