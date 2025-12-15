
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ProjectGrid extends GridBase<ProjectRow, any> {
        protected getColumnsKey() { return 'Masters.Project'; }
        protected getDialogType() { return ProjectDialog; }
        protected getIdProperty() { return ProjectRow.idProperty; }
        protected getInsertPermission() { return ProjectRow.insertPermission; }
        protected getLocalTextPrefix() { return ProjectRow.localTextPrefix; }
        protected getService() { return ProjectService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}