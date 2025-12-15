
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailJustDialDialog extends DialogBase<BizMailJustDialRow, any> {
        protected getFormKey() { return BizMailJustDialForm.formKey; }
        protected getIdProperty() { return BizMailJustDialRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailJustDialRow.localTextPrefix; }
        protected getNameProperty() { return BizMailJustDialRow.nameProperty; }
        protected getService() { return BizMailJustDialService.baseUrl; }
        protected getDeletePermission() { return BizMailJustDialRow.deletePermission; }
        protected getInsertPermission() { return BizMailJustDialRow.insertPermission; }
        protected getUpdatePermission() { return BizMailJustDialRow.updatePermission; }

        protected form = new BizMailJustDialForm(this.idPrefix);

    }
}