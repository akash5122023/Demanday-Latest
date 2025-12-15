
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIvrDialog extends DialogBase<BizMailIvrRow, any> {
        protected getFormKey() { return BizMailIvrForm.formKey; }
        protected getIdProperty() { return BizMailIvrRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailIvrRow.localTextPrefix; }
        protected getNameProperty() { return BizMailIvrRow.nameProperty; }
        protected getService() { return BizMailIvrService.baseUrl; }
        protected getDeletePermission() { return BizMailIvrRow.deletePermission; }
        protected getInsertPermission() { return BizMailIvrRow.insertPermission; }
        protected getUpdatePermission() { return BizMailIvrRow.updatePermission; }

        protected form = new BizMailIvrForm(this.idPrefix);

    }
}