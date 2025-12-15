
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailWebDialog extends DialogBase<BizMailWebRow, any> {
        protected getFormKey() { return BizMailWebForm.formKey; }
        protected getIdProperty() { return BizMailWebRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailWebRow.localTextPrefix; }
        protected getNameProperty() { return BizMailWebRow.nameProperty; }
        protected getService() { return BizMailWebService.baseUrl; }
        protected getDeletePermission() { return BizMailWebRow.deletePermission; }
        protected getInsertPermission() { return BizMailWebRow.insertPermission; }
        protected getUpdatePermission() { return BizMailWebRow.updatePermission; }

        protected form = new BizMailWebForm(this.idPrefix);

    }
}