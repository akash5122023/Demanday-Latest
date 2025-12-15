
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class TabGenderGrid extends Serenity.EntityGrid<TabGenderRow, any> {
        protected getColumnsKey() { return TabGenderColumns.columnsKey; }
        protected getDialogType() { return TabGenderDialog; }
        protected getIdProperty() { return TabGenderRow.idProperty; }
        protected getInsertPermission() { return TabGenderRow.insertPermission; }
        protected getLocalTextPrefix() { return TabGenderRow.localTextPrefix; }
        protected getService() { return TabGenderService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}