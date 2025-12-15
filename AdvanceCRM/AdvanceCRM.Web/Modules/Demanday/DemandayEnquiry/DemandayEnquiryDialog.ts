
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
        @Serenity.Decorators.panel()
    export class DemandayEnquiryDialog extends DialogBase<DemandayEnquiryRow, any> {
        protected getFormKey() { return DemandayEnquiryForm.formKey; }
        protected getIdProperty() { return DemandayEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return DemandayEnquiryRow.nameProperty; }
        protected getService() { return DemandayEnquiryService.baseUrl; }
        protected getDeletePermission() { return DemandayEnquiryRow.deletePermission; }
        protected getInsertPermission() { return DemandayEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return DemandayEnquiryRow.updatePermission; }

        protected form = new DemandayEnquiryForm(this.idPrefix);

    }
}