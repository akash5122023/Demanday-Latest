
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIstamojoDialog extends DialogBase<BizMailIstamojoRow, any> {
        protected getFormKey() { return BizMailIstamojoForm.formKey; }
        protected getIdProperty() { return BizMailIstamojoRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailIstamojoRow.localTextPrefix; }
        protected getNameProperty() { return BizMailIstamojoRow.nameProperty; }
        protected getService() { return BizMailIstamojoService.baseUrl; }
        protected getDeletePermission() { return BizMailIstamojoRow.deletePermission; }
        protected getInsertPermission() { return BizMailIstamojoRow.insertPermission; }
        protected getUpdatePermission() { return BizMailIstamojoRow.updatePermission; }

        protected form = new BizMailIstamojoForm(this.idPrefix);

    }
}