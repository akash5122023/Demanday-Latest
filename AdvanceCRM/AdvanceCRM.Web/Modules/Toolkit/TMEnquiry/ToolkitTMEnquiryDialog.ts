
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class ToolkitTMEnquiryDialog extends DialogBase<ToolkitTMEnquiryRow, any> {
        protected getFormKey() { return ToolkitTMEnquiryForm.formKey; }
        protected getIdProperty() { return ToolkitTMEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return ToolkitTMEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return ToolkitTMEnquiryRow.nameProperty; }
        protected getService() { return ToolkitTMEnquiryService.baseUrl; }
        protected getDeletePermission() { return ToolkitTMEnquiryRow.deletePermission; }
        protected getInsertPermission() { return ToolkitTMEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return ToolkitTMEnquiryRow.updatePermission; }

        protected form = new ToolkitTMEnquiryForm(this.idPrefix);

    }
}
