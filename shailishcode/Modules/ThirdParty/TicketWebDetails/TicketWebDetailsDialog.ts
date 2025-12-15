
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class TicketWebDetailsDialog extends Serenity.EntityDialog<TicketWebDetailsRow, any> {
        protected getFormKey() { return TicketWebDetailsForm.formKey; }
        protected getIdProperty() { return TicketWebDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return TicketWebDetailsRow.localTextPrefix; }
        protected getNameProperty() { return TicketWebDetailsRow.nameProperty; }
        protected getService() { return TicketWebDetailsService.baseUrl; }
        protected getDeletePermission() { return TicketWebDetailsRow.deletePermission; }
        protected getInsertPermission() { return TicketWebDetailsRow.insertPermission; }
        protected getUpdatePermission() { return TicketWebDetailsRow.updatePermission; }

        protected form = new TicketWebDetailsForm(this.idPrefix);

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();
            //buttons.shift();
            //buttons.shift();
            //buttons.shift();

            buttons.push({
                title: 'To Ticket',
                icon: 'fa-share-square text-yellow',
                hint: 'Move to Ticket',
                onClick: () =>{
                    TicketWebDetailsService.MoveToTicket({ //Disable if not applicable
                        Id: this.get_entityId()
                    },
                        response => {
                            if (response.Id > 0) {
                                Q.reloadLookup(Contacts.ContactsRow.lookupKey);
                                Q.reloadLookup(Contacts.SubContactsRow.lookupKey);
                                Q.notifyInfo(response.Status);

                                new Services.TicketDialog().loadByIdAndOpenDialog(response.Id);
                            }
                            else
                                Q.notifyError(response.Status)
                        }
                    );
                }
            });

            return buttons;
        }

    }
}