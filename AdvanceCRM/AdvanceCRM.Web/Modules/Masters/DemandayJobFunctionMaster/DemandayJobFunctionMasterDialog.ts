
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayJobFunctionMasterDialog extends Serenity.EntityDialog<DemandayJobFunctionMasterRow, any> {
        protected getFormKey() { return DemandayJobFunctionMasterForm.formKey; }
        protected getIdProperty() { return DemandayJobFunctionMasterRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayJobFunctionMasterRow.localTextPrefix; }
        protected getNameProperty() { return DemandayJobFunctionMasterRow.nameProperty; }
        protected getService() { return DemandayJobFunctionMasterService.baseUrl; }
        protected getDeletePermission() { return DemandayJobFunctionMasterRow.deletePermission; }
        protected getInsertPermission() { return DemandayJobFunctionMasterRow.insertPermission; }
        protected getUpdatePermission() { return DemandayJobFunctionMasterRow.updatePermission; }

        protected form = new DemandayJobFunctionMasterForm(this.idPrefix);

    }
}