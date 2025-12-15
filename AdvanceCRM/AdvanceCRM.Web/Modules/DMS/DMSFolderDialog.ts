
namespace AdvanceCRM.DMS {

    @Serenity.Decorators.registerClass()
    export class DMSFolderDialog extends Serenity.EntityDialog<DMSRow, any> {
        protected getFormKey() { return DMSFolderForm.formKey; }
        protected getIdProperty() { return DMSRow.idProperty; }
        protected getLocalTextPrefix() { return DMSRow.localTextPrefix; }
        protected getNameProperty() { return DMSRow.nameProperty; }
        protected getService() { return DMSService.baseUrl; }
        protected getDeletePermission() { return DMSRow.deletePermission; }
        protected getInsertPermission() { return DMSRow.insertPermission; }
        protected getUpdatePermission() { return DMSRow.updatePermission; }

        protected form = new DMSFolderForm(this.idPrefix);

        constructor() {
            super();
        }

        updateInterface() {
            super.updateInterface();
            Serenity.EditorUtils.setReadOnly(this.form.ParentId, true);
        }

        protected updateTitle() {
            this.dialogTitle = "New Folder";
        }

        onDialogOpen() {
            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
        }

    }
}