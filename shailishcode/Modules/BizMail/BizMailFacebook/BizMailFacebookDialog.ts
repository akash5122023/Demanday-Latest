
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailFacebookDialog extends DialogBase<BizMailFacebookRow, any> {
        protected getFormKey() { return BizMailFacebookForm.formKey; }
        protected getIdProperty() { return BizMailFacebookRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailFacebookRow.localTextPrefix; }
        protected getNameProperty() { return BizMailFacebookRow.nameProperty; }
        protected getService() { return BizMailFacebookService.baseUrl; }
        protected getDeletePermission() { return BizMailFacebookRow.deletePermission; }
        protected getInsertPermission() { return BizMailFacebookRow.insertPermission; }
        protected getUpdatePermission() { return BizMailFacebookRow.updatePermission; }

        protected form = new BizMailFacebookForm(this.idPrefix);

    }
}