
namespace AdvanceCRM.EBBCheck {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class EBBCheckDialog extends DialogBase<EBBCheckRow, any> {
        protected getFormKey() { return EBBCheckForm.formKey; }
        protected getIdProperty() { return EBBCheckRow.idProperty; }
        protected getLocalTextPrefix() { return EBBCheckRow.localTextPrefix; }
        protected getNameProperty() { return EBBCheckRow.nameProperty; }
        protected getService() { return EBBCheckService.baseUrl; }
        protected getDeletePermission() { return EBBCheckRow.deletePermission; }
        protected getInsertPermission() { return EBBCheckRow.insertPermission; }
        protected getUpdatePermission() { return EBBCheckRow.updatePermission; }

        protected form = new EBBCheckForm(this.idPrefix);

        updateInterface() {
            super.updateInterface();

            // Status is a Quality-team-only field. Anyone without EBBCheck:ChangeStatus
            // sees it read-only (the server enforces this too – EBBCheckSaveHandler).
            var canChangeStatus = Q.Authorization.hasPermission("EBBCheck:ChangeStatus");
            Serenity.EditorUtils.setReadOnly(this.form.Status, !canChangeStatus);

            // First Name / Email / Date can only be entered while creating the record.
            // Once saved they are locked (the server also keeps the old values on update).
            var isEdit = !this.isNew();
            Serenity.EditorUtils.setReadOnly(this.form.FirstName, isEdit);
            Serenity.EditorUtils.setReadOnly(this.form.Email, isEdit);
            Serenity.EditorUtils.setReadOnly(this.form.Date, isEdit);
        }
    }
}
