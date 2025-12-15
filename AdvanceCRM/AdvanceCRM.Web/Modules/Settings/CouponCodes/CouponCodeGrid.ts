namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class CouponCodeGrid extends GridBase<CouponCodeRow, any> {
        protected getColumnsKey() { return 'Settings.CouponCode'; }
        protected getDialogType() { return CouponCodeDialog; }
        protected getIdProperty() { return CouponCodeRow.idProperty; }
        protected getInsertPermission() { return CouponCodeRow.insertPermission; }
        protected getLocalTextPrefix() { return CouponCodeRow.localTextPrefix; }
        protected getService() { return 'Settings/CouponCodes'; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}
