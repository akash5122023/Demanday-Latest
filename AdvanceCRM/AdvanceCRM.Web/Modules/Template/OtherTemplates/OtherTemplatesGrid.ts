
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class OtherTemplatesGrid extends GridBase<OtherTemplatesRow, any> {
        protected getColumnsKey() { return 'Template.OtherTemplates'; }
        protected getDialogType() { return OtherTemplatesDialog; }
        protected getIdProperty() { return OtherTemplatesRow.idProperty; }
        protected getInsertPermission() { return OtherTemplatesRow.insertPermission; }
        protected getLocalTextPrefix() { return OtherTemplatesRow.localTextPrefix; }
        protected getService() { return OtherTemplatesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}