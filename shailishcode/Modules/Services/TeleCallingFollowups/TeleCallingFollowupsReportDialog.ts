
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingFollowupsReportDialog extends ReadOnlyDialog<TeleCallingFollowupsRow> {
        protected getFormKey() { return TeleCallingFollowupsForm.formKey; }
        protected getIdProperty() { return TeleCallingFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingFollowupsRow.nameProperty; }
        protected getService() { return TeleCallingFollowupsService.baseUrl; }
        protected getDeletePermission() { return TeleCallingFollowupsRow.deletePermission; }
        protected getInsertPermission() { return TeleCallingFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return TeleCallingFollowupsRow.updatePermission; }

        protected form = new TeleCallingFollowupsForm(this.idPrefix);

    }
}