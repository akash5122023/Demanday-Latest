
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailTaskDialog extends DialogBase<BizMailTaskRow, any> {
        protected getFormKey() { return BizMailTaskForm.formKey; }
        protected getIdProperty() { return BizMailTaskRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailTaskRow.localTextPrefix; }
        protected getNameProperty() { return BizMailTaskRow.nameProperty; }
        protected getService() { return BizMailTaskService.baseUrl; }
        protected getDeletePermission() { return BizMailTaskRow.deletePermission; }
        protected getInsertPermission() { return BizMailTaskRow.insertPermission; }
        protected getUpdatePermission() { return BizMailTaskRow.updatePermission; }

        protected form = new BizMailTaskForm(this.idPrefix);

    }
}