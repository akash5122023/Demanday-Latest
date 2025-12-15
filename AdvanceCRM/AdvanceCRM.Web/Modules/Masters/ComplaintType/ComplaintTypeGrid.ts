

namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ComplaintTypeGrid extends GridBase<ComplaintTypeRow, any> {
        protected getColumnsKey() { return 'Masters.ComplaintType'; }
        protected getDialogType() { return ComplaintTypeDialog; }
        protected getIdProperty() { return ComplaintTypeRow.idProperty; }
        protected getInsertPermission() { return ComplaintTypeRow.insertPermission; }
        protected getLocalTextPrefix() { return ComplaintTypeRow.localTextPrefix; }
        protected getService() { return ComplaintTypeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}