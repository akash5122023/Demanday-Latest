namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class CouponCodeDialog extends Serenity.EntityDialog<CouponCodeRow, any> {
        protected getFormKey() { return CouponCodeForm.formKey; }
        protected getIdProperty() { return CouponCodeRow.idProperty; }
        protected getLocalTextPrefix() { return CouponCodeRow.localTextPrefix; }
        protected getNameProperty() { return CouponCodeRow.nameProperty; }
        protected getService() { return 'Settings/CouponCodes'; }
        protected getDeletePermission() { return CouponCodeRow.deletePermission; }
        protected getInsertPermission() { return CouponCodeRow.insertPermission; }
        protected getUpdatePermission() { return CouponCodeRow.updatePermission; }

        protected form = new CouponCodeForm(this.idPrefix);
    }
}
