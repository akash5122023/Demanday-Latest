
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationFollowupsReportDialog extends ReadOnlyDialog<QuotationFollowupsRow> {
        protected getFormKey() { return QuotationFollowupsForm.formKey; }
        protected getIdProperty() { return QuotationFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return QuotationFollowupsRow.nameProperty; }
        protected getService() { return QuotationFollowupsService.baseUrl; }
        protected getDeletePermission() { return QuotationFollowupsRow.deletePermission; }
        protected getInsertPermission() { return QuotationFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return QuotationFollowupsRow.updatePermission; }

        protected form = new QuotationFollowupsForm(this.idPrefix);

    }
}