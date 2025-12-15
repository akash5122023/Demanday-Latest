
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailVisitDialog extends DialogBase<BizMailVisitRow, any> {
        protected getFormKey() { return BizMailVisitForm.formKey; }
        protected getIdProperty() { return BizMailVisitRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailVisitRow.localTextPrefix; }
        protected getNameProperty() { return BizMailVisitRow.nameProperty; }
        protected getService() { return BizMailVisitService.baseUrl; }
        protected getDeletePermission() { return BizMailVisitRow.deletePermission; }
        protected getInsertPermission() { return BizMailVisitRow.insertPermission; }
        protected getUpdatePermission() { return BizMailVisitRow.updatePermission; }

        protected form = new BizMailVisitForm(this.idPrefix);

    }
}