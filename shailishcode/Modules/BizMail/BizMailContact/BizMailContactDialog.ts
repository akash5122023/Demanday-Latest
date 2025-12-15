
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailContactDialog extends DialogBase<BizMailContactRow, any> {
        protected getFormKey() { return BizMailContactForm.formKey; }
        protected getIdProperty() { return BizMailContactRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailContactRow.localTextPrefix; }
        protected getNameProperty() { return BizMailContactRow.nameProperty; }
        protected getService() { return BizMailContactService.baseUrl; }
        protected getDeletePermission() { return BizMailContactRow.deletePermission; }
        protected getInsertPermission() { return BizMailContactRow.insertPermission; }
        protected getUpdatePermission() { return BizMailContactRow.updatePermission; }

        protected form = new BizMailContactForm(this.idPrefix);

    }
}