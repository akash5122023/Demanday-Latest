
namespace AdvanceCRM.DMS {

    @Serenity.Decorators.registerClass()
    export class DMSDialog extends DialogBase<DMSRow, any> {
        protected getFormKey() { return DMSForm.formKey; }
        protected getIdProperty() { return DMSRow.idProperty; }
        protected getLocalTextPrefix() { return DMSRow.localTextPrefix; }
        protected getNameProperty() { return DMSRow.nameProperty; }
        protected getService() { return DMSService.baseUrl; }
        protected getDeletePermission() { return DMSRow.deletePermission; }
        protected getInsertPermission() { return DMSRow.insertPermission; }
        protected getUpdatePermission() { return DMSRow.updatePermission; }

        protected form = new DMSForm(this.idPrefix);

        constructor() {
            super();
        }

        onSaveSuccess(response) {
            super.onSaveSuccess(response);
            Q.reloadLookup("DMS.DMS");
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'New Folder',
                cssClass: 'add-button',
                icon: 'fa-plus-circle text-green',
                onClick: () => {
                    var dlg = new DMSFolderDialog();
                    dlg.loadEntityAndOpenDialog({ ParentId: this.entityId });
                },
                separator: true
            });

            return buttons;
        }

        onDialogOpen() {
            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.LastUpdatedId.value < "1") {
                this.form.LastUpdatedId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }
        }
    }
}