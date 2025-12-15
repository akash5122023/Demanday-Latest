

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class StateGrid extends GridBase<StateRow, any> {
        protected getColumnsKey() { return 'Masters.State'; }
        protected getDialogType() { return StateDialog; }
        protected getIdProperty() { return StateRow.idProperty; }
        protected getInsertPermission() { return StateRow.insertPermission; }
        protected getLocalTextPrefix() { return StateRow.localTextPrefix; }
        protected getService() { return StateService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}