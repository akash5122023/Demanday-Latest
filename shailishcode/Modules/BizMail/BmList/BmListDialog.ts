
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmListDialog extends DialogBase<BmListRow, any> {
        protected getFormKey() { return BmListForm.formKey; }
        protected getIdProperty() { return BmListRow.idProperty; }
        protected getLocalTextPrefix() { return BmListRow.localTextPrefix; }
        protected getNameProperty() { return BmListRow.nameProperty; }
        protected getService() { return BmListService.baseUrl; }
        protected getDeletePermission() { return BmListRow.deletePermission; }
        protected getInsertPermission() { return BmListRow.insertPermission; }
        protected getUpdatePermission() { return BmListRow.updatePermission; }

        protected form = new BmListForm(this.idPrefix);

    }
}