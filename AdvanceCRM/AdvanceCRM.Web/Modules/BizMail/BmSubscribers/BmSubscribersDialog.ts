
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmSubscribersDialog extends DialogBase<BmSubscribersRow, any> {
        protected getFormKey() { return BmSubscribersForm.formKey; }
        protected getIdProperty() { return BmSubscribersRow.idProperty; }
        protected getLocalTextPrefix() { return BmSubscribersRow.localTextPrefix; }
        protected getNameProperty() { return BmSubscribersRow.nameProperty; }
        protected getService() { return BmSubscribersService.baseUrl; }
        protected getDeletePermission() { return BmSubscribersRow.deletePermission; }
        protected getInsertPermission() { return BmSubscribersRow.insertPermission; }
        protected getUpdatePermission() { return BmSubscribersRow.updatePermission; }

        protected form = new BmSubscribersForm(this.idPrefix);

    }
}