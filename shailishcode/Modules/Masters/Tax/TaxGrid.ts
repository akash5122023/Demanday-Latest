

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TaxGrid extends GridBase<TaxRow, any> {
        protected getColumnsKey() { return 'Masters.Tax'; }
        protected getDialogType() { return TaxDialog; }
        protected getIdProperty() { return TaxRow.idProperty; }
        protected getInsertPermission() { return TaxRow.insertPermission; }
        protected getLocalTextPrefix() { return TaxRow.localTextPrefix; }
        protected getService() { return TaxService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}