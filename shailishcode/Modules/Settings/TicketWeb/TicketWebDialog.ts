/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class TicketWebDialog extends ConfigurationDialog<TicketWebRow> {
        protected getFormKey() { return TicketWebForm.formKey; }
        protected getIdProperty() { return TicketWebRow.idProperty; }
        protected getLocalTextPrefix() { return TicketWebRow.localTextPrefix; }
        protected getNameProperty() { return TicketWebRow.nameProperty; }
        protected getService() { return TicketWebService.baseUrl; }
        protected getDeletePermission() { return TicketWebRow.deletePermission; }
        protected getInsertPermission() { return TicketWebRow.insertPermission; }
        protected getUpdatePermission() { return TicketWebRow.updatePermission; }

        protected form = new TicketWebForm(this.idPrefix);

    }



}