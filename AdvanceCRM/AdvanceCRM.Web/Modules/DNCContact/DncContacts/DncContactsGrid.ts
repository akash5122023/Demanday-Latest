
namespace AdvanceCRM.DNCContact {

    @Serenity.Decorators.registerClass()
    export class DncContactsGrid extends GridBase<DncContactsRow, any> {
        protected getColumnsKey() { return DncContactsColumns.columnsKey; }
        protected getDialogType() { return DncContactsDialog; }
        protected getIdProperty() { return DncContactsRow.idProperty; }
        protected getInsertPermission() { return DncContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return DncContactsRow.localTextPrefix; }
        protected getService() { return DncContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}