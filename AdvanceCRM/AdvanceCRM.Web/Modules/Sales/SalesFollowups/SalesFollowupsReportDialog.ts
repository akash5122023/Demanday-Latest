
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class SalesFollowupsReportDialog extends ReadOnlyDialog<SalesFollowupsRow> {
        protected getFormKey() { return SalesFollowupsForm.formKey; }
        protected getIdProperty() { return SalesFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return SalesFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return SalesFollowupsRow.nameProperty; }
        protected getService() { return SalesFollowupsService.baseUrl; }
        protected getDeletePermission() { return SalesFollowupsRow.deletePermission; }
        protected getInsertPermission() { return SalesFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return SalesFollowupsRow.updatePermission; }

        protected form = new SalesFollowupsForm(this.idPrefix);

    }
}