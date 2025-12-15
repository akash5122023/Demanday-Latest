
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DealerGrid extends GridBase<DealerRow, any> {
        protected getColumnsKey() { return 'Masters.Dealer'; }
        protected getDialogType() { return DealerDialog; }
        protected getIdProperty() { return DealerRow.idProperty; }
        protected getInsertPermission() { return DealerRow.insertPermission; }
        protected getLocalTextPrefix() { return DealerRow.localTextPrefix; }
        protected getService() { return DealerService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}