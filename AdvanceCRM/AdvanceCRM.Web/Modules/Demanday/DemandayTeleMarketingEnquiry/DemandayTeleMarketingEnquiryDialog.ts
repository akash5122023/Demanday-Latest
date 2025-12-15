
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingEnquiryDialog extends DialogBase<DemandayTeleMarketingEnquiryRow, any> {
        protected getFormKey() { return DemandayTeleMarketingEnquiryForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingEnquiryRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingEnquiryService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingEnquiryRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingEnquiryRow.updatePermission; }

        protected form = new DemandayTeleMarketingEnquiryForm(this.idPrefix);

    }
}