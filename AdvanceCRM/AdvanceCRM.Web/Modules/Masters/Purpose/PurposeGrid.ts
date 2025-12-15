
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class PurposeGrid extends GridBase<PurposeRow, any> {
        protected getColumnsKey() { return 'Masters.Purpose'; }
        protected getDialogType() { return PurposeDialog; }
        protected getIdProperty() { return PurposeRow.idProperty; }
        protected getInsertPermission() { return PurposeRow.insertPermission; }
        protected getLocalTextPrefix() { return PurposeRow.localTextPrefix; }
        protected getService() { return PurposeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}