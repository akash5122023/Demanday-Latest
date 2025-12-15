
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class QuickMailTemplateGrid extends Serenity.EntityGrid<QuickMailTemplateRow, any> {
        protected getColumnsKey() { return 'Template.QuickMailTemplate'; }
        protected getDialogType() { return QuickMailTemplateDialog; }
        protected getIdProperty() { return QuickMailTemplateRow.idProperty; }
        protected getInsertPermission() { return QuickMailTemplateRow.insertPermission; }
        protected getLocalTextPrefix() { return QuickMailTemplateRow.localTextPrefix; }
        protected getService() { return QuickMailTemplateService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}