

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class GradeGrid extends GridBase<GradeRow, any> {
        protected getColumnsKey() { return 'Masters.Grade'; }
        protected getDialogType() { return GradeDialog; }
        protected getIdProperty() { return GradeRow.idProperty; }
        protected getInsertPermission() { return GradeRow.insertPermission; }
        protected getLocalTextPrefix() { return GradeRow.localTextPrefix; }
        protected getService() { return GradeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}