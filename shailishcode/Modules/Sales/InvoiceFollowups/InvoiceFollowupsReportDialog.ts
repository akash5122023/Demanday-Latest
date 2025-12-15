
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceFollowupsReportDialog extends ReadOnlyDialog<InvoiceFollowupsRow> {
        protected getFormKey() { return InvoiceFollowupsForm.formKey; }
        protected getIdProperty() { return InvoiceFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceFollowupsRow.nameProperty; }
        protected getService() { return InvoiceFollowupsService.baseUrl; }
        protected getDeletePermission() { return InvoiceFollowupsRow.deletePermission; }
        protected getInsertPermission() { return InvoiceFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return InvoiceFollowupsRow.updatePermission; }

        protected form = new InvoiceFollowupsForm(this.idPrefix);
        
    }
}