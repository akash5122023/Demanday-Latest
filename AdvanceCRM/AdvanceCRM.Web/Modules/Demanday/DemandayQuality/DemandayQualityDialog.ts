
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayQualityDialog extends DialogBase<DemandayQualityRow, any> {
        protected getFormKey() { return DemandayQualityForm.formKey; }
        protected getIdProperty() { return DemandayQualityRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayQualityRow.localTextPrefix; }
        protected getNameProperty() { return DemandayQualityRow.nameProperty; }
        protected getService() { return DemandayQualityService.baseUrl; }
        protected getDeletePermission() { return DemandayQualityRow.deletePermission; }
        protected getInsertPermission() { return DemandayQualityRow.insertPermission; }
        protected getUpdatePermission() { return DemandayQualityRow.updatePermission; }

        protected form = new DemandayQualityForm(this.idPrefix);

    }
}