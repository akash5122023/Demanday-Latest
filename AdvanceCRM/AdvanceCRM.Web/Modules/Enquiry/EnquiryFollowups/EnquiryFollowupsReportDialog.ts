
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryFollowupsReportDialog extends ReadOnlyDialog<EnquiryFollowupsRow> {
        protected getFormKey() { return EnquiryFollowupsForm.formKey; }
        protected getIdProperty() { return EnquiryFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryFollowupsRow.nameProperty; }
        protected getService() { return EnquiryFollowupsService.baseUrl; }
        protected getDeletePermission() { return EnquiryFollowupsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryFollowupsRow.updatePermission; }

        protected form = new EnquiryFollowupsForm(this.idPrefix);
    }
}