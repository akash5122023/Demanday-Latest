
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailWooComGrid extends GridBase<BizMailWooComRow, any> {
        protected getColumnsKey() { return 'BizMail.BizMailWooCom'; }
        protected getDialogType() { return BizMailWooComDialog; }
        protected getIdProperty() { return BizMailWooComRow.idProperty; }
        protected getInsertPermission() { return BizMailWooComRow.insertPermission; }
        protected getLocalTextPrefix() { return BizMailWooComRow.localTextPrefix; }
        protected getService() { return BizMailWooComService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}