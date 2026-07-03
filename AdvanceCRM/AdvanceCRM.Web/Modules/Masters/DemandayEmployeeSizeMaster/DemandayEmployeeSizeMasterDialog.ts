
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayEmployeeSizeMasterDialog extends Serenity.EntityDialog<DemandayEmployeeSizeMasterRow, any> {
        protected getFormKey() { return DemandayEmployeeSizeMasterForm.formKey; }
        protected getIdProperty() { return DemandayEmployeeSizeMasterRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayEmployeeSizeMasterRow.localTextPrefix; }
        protected getNameProperty() { return DemandayEmployeeSizeMasterRow.nameProperty; }
        protected getService() { return DemandayEmployeeSizeMasterService.baseUrl; }
        protected getDeletePermission() { return DemandayEmployeeSizeMasterRow.deletePermission; }
        protected getInsertPermission() { return DemandayEmployeeSizeMasterRow.insertPermission; }
        protected getUpdatePermission() { return DemandayEmployeeSizeMasterRow.updatePermission; }

        protected form = new DemandayEmployeeSizeMasterForm(this.idPrefix);

    }
}