

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class CategoryGrid extends GridBase<CategoryRow, any> {
        protected getColumnsKey() { return 'Masters.Category'; }
        protected getDialogType() { return CategoryDialog; }
        protected getIdProperty() { return CategoryRow.idProperty; }
        protected getInsertPermission() { return CategoryRow.insertPermission; }
        protected getLocalTextPrefix() { return CategoryRow.localTextPrefix; }
        protected getService() { return CategoryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}