
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class CMSFollowupsReportDialog extends ReadOnlyDialog<CMSFollowupsRow> {
        protected getFormKey() { return CMSFollowupsForm.formKey; }
        protected getIdProperty() { return CMSFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return CMSFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return CMSFollowupsRow.nameProperty; }
        protected getService() { return CMSFollowupsService.baseUrl; }
        protected getDeletePermission() { return CMSFollowupsRow.deletePermission; }
        protected getInsertPermission() { return CMSFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return CMSFollowupsRow.updatePermission; }

        protected form = new CMSFollowupsForm(this.idPrefix);

    }
}
