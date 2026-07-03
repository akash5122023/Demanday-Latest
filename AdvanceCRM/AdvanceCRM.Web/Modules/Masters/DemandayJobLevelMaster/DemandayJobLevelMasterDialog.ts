
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayJobLevelMasterDialog extends Serenity.EntityDialog<DemandayJobLevelMasterRow, any> {
        protected getFormKey() { return DemandayJobLevelMasterForm.formKey; }
        protected getIdProperty() { return DemandayJobLevelMasterRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayJobLevelMasterRow.localTextPrefix; }
        protected getNameProperty() { return DemandayJobLevelMasterRow.nameProperty; }
        protected getService() { return DemandayJobLevelMasterService.baseUrl; }
        protected getDeletePermission() { return DemandayJobLevelMasterRow.deletePermission; }
        protected getInsertPermission() { return DemandayJobLevelMasterRow.insertPermission; }
        protected getUpdatePermission() { return DemandayJobLevelMasterRow.updatePermission; }

        protected form = new DemandayJobLevelMasterForm(this.idPrefix);

    }
}