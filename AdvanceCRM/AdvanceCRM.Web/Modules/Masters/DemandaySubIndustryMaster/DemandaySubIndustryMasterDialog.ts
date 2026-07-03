
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandaySubIndustryMasterDialog extends Serenity.EntityDialog<DemandaySubIndustryMasterRow, any> {
        protected getFormKey() { return DemandaySubIndustryMasterForm.formKey; }
        protected getIdProperty() { return DemandaySubIndustryMasterRow.idProperty; }
        protected getLocalTextPrefix() { return DemandaySubIndustryMasterRow.localTextPrefix; }
        protected getNameProperty() { return DemandaySubIndustryMasterRow.nameProperty; }
        protected getService() { return DemandaySubIndustryMasterService.baseUrl; }
        protected getDeletePermission() { return DemandaySubIndustryMasterRow.deletePermission; }
        protected getInsertPermission() { return DemandaySubIndustryMasterRow.insertPermission; }
        protected getUpdatePermission() { return DemandaySubIndustryMasterRow.updatePermission; }

        protected form = new DemandaySubIndustryMasterForm(this.idPrefix);

    }
}