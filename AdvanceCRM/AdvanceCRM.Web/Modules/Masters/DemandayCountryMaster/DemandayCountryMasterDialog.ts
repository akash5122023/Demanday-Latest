
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayCountryMasterDialog extends Serenity.EntityDialog<DemandayCountryMasterRow, any> {
        protected getFormKey() { return DemandayCountryMasterForm.formKey; }
        protected getIdProperty() { return DemandayCountryMasterRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayCountryMasterRow.localTextPrefix; }
        protected getNameProperty() { return DemandayCountryMasterRow.nameProperty; }
        protected getService() { return DemandayCountryMasterService.baseUrl; }
        protected getDeletePermission() { return DemandayCountryMasterRow.deletePermission; }
        protected getInsertPermission() { return DemandayCountryMasterRow.insertPermission; }
        protected getUpdatePermission() { return DemandayCountryMasterRow.updatePermission; }

        protected form = new DemandayCountryMasterForm(this.idPrefix);

    }
}