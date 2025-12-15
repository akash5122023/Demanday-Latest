

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AreaGrid extends GridBase<AreaRow, any> {
        protected getColumnsKey() { return 'Masters.Area'; }
        protected getDialogType() { return AreaDialog; }
        protected getIdProperty() { return AreaRow.idProperty; }
        protected getInsertPermission() { return AreaRow.insertPermission; }
        protected getLocalTextPrefix() { return AreaRow.localTextPrefix; }
        protected getService() { return AreaService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}