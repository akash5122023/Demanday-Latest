
/// <reference path="../../Common/Helpers/ReadOnlyDialog.ts" />
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class TicketDialog extends DialogBase<TicketRow, any> {
        protected getFormKey() { return TicketForm.formKey; }
        protected getIdProperty() { return TicketRow.idProperty; }
        protected getLocalTextPrefix() { return TicketRow.localTextPrefix; }
        protected getNameProperty() { return TicketRow.nameProperty; }
        protected getService() { return TicketService.baseUrl; }
        protected getDeletePermission() { return TicketRow.deletePermission; }
        protected getInsertPermission() { return TicketRow.insertPermission; }
        protected getUpdatePermission() { return TicketRow.updatePermission; }

        protected form = new TicketForm(this.idPrefix);


        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

           
            if (Authorization.hasPermission("Ticket:Move to CMS")) {
                buttons.push({
                    title: 'To CMS',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Move To CMS',
                    onClick: () => {
                        TicketService.MoveToCMS({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Services.CMSDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    },
                    separator: true
                });
            }

            return buttons;
        }

    }
}