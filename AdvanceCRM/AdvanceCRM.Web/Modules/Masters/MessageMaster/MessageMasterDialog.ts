
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class MessageMasterDialog extends Serenity.EntityDialog<MessageMasterRow, any> {
        protected getFormKey() { return MessageMasterForm.formKey; }
        protected getIdProperty() { return MessageMasterRow.idProperty; }
        protected getLocalTextPrefix() { return MessageMasterRow.localTextPrefix; }
        protected getNameProperty() { return MessageMasterRow.nameProperty; }
        protected getService() { return MessageMasterService.baseUrl; }
        protected getDeletePermission() { return MessageMasterRow.deletePermission; }
        protected getInsertPermission() { return MessageMasterRow.insertPermission; }
        protected getUpdatePermission() { return MessageMasterRow.updatePermission; }

        protected form = new MessageMasterForm(this.idPrefix);

    }
}