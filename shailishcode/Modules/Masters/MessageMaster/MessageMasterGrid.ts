
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class MessageMasterGrid extends Serenity.EntityGrid<MessageMasterRow, any> {
        protected getColumnsKey() { return 'Masters.MessageMaster'; }
        protected getDialogType() { return MessageMasterDialog; }
        protected getIdProperty() { return MessageMasterRow.idProperty; }
        protected getInsertPermission() { return MessageMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return MessageMasterRow.localTextPrefix; }
        protected getService() { return MessageMasterService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}