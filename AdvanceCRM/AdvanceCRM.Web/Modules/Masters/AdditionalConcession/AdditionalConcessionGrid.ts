

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalConcessionGrid extends GridBase<AdditionalConcessionRow, any> {
        protected getColumnsKey() { return 'Masters.AdditionalConcession'; }
        protected getDialogType() { return AdditionalConcessionDialog; }
        protected getIdProperty() { return AdditionalConcessionRow.idProperty; }
        protected getInsertPermission() { return AdditionalConcessionRow.insertPermission; }
        protected getLocalTextPrefix() { return AdditionalConcessionRow.localTextPrefix; }
        protected getService() { return AdditionalConcessionService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}