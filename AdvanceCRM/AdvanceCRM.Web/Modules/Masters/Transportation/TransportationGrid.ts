

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TransportationGrid extends GridBase<TransportationRow, any> {
        protected getColumnsKey() { return 'Masters.Transportation'; }
        protected getDialogType() { return TransportationDialog; }
        protected getIdProperty() { return TransportationRow.idProperty; }
        protected getInsertPermission() { return TransportationRow.insertPermission; }
        protected getLocalTextPrefix() { return TransportationRow.localTextPrefix; }
        protected getService() { return TransportationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}