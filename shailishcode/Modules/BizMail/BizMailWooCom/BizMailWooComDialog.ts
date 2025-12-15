
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailWooComDialog extends DialogBase<BizMailWooComRow, any> {
        protected getFormKey() { return BizMailWooComForm.formKey; }
        protected getIdProperty() { return BizMailWooComRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailWooComRow.localTextPrefix; }
        protected getNameProperty() { return BizMailWooComRow.nameProperty; }
        protected getService() { return BizMailWooComService.baseUrl; }
        protected getDeletePermission() { return BizMailWooComRow.deletePermission; }
        protected getInsertPermission() { return BizMailWooComRow.insertPermission; }
        protected getUpdatePermission() { return BizMailWooComRow.updatePermission; }

        protected form = new BizMailWooComForm(this.idPrefix);

    }
}