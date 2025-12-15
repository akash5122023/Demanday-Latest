
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class PurposeDialog extends DialogBase<PurposeRow, any> {
        protected getFormKey() { return PurposeForm.formKey; }
        protected getIdProperty() { return PurposeRow.idProperty; }
        protected getLocalTextPrefix() { return PurposeRow.localTextPrefix; }
        protected getNameProperty() { return PurposeRow.nameProperty; }
        protected getService() { return PurposeService.baseUrl; }
        protected getDeletePermission() { return PurposeRow.deletePermission; }
        protected getInsertPermission() { return PurposeRow.insertPermission; }
        protected getUpdatePermission() { return PurposeRow.updatePermission; }

        protected form = new PurposeForm(this.idPrefix);

    }
}