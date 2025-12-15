
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailCmsDialog extends DialogBase<BizMailCmsRow, any> {
        protected getFormKey() { return BizMailCmsForm.formKey; }
        protected getIdProperty() { return BizMailCmsRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailCmsRow.localTextPrefix; }
        protected getNameProperty() { return BizMailCmsRow.nameProperty; }
        protected getService() { return BizMailCmsService.baseUrl; }
        protected getDeletePermission() { return BizMailCmsRow.deletePermission; }
        protected getInsertPermission() { return BizMailCmsRow.insertPermission; }
        protected getUpdatePermission() { return BizMailCmsRow.updatePermission; }

        protected form = new BizMailCmsForm(this.idPrefix);

    }
}