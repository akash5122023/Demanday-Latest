
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DaysGrid extends Serenity.EntityGrid<DaysRow, any> {
        protected getColumnsKey() { return 'Masters.Days'; }
        protected getDialogType() { return DaysDialog; }
        protected getIdProperty() { return DaysRow.idProperty; }
        protected getInsertPermission() { return DaysRow.insertPermission; }
        protected getLocalTextPrefix() { return DaysRow.localTextPrefix; }
        protected getService() { return DaysService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}