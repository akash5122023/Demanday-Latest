
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class MailInboxDetailsDialog extends DialogBase<MailInboxDetailsRow, any> {
        protected getFormKey() { return MailInboxDetailsForm.formKey; }
        protected getIdProperty() { return MailInboxDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return MailInboxDetailsRow.localTextPrefix; }
        protected getNameProperty() { return MailInboxDetailsRow.nameProperty; }
        protected getService() { return MailInboxDetailsService.baseUrl; }
        protected getDeletePermission() { return MailInboxDetailsRow.deletePermission; }
        protected getInsertPermission() { return MailInboxDetailsRow.insertPermission; }
        protected getUpdatePermission() { return MailInboxDetailsRow.updatePermission; }

        protected form = new MailInboxDetailsForm(this.idPrefix);

        protected updateInterface(): void {
            super.updateInterface();
            //Serenity.EditorUtils.setReadonly(this.element.find('.editor'), false);
            //this.element.find('sup').hide();
            if (this.form.IsMoved.value == true)
                this.toolbar.element.toggle(false);
        }

        protected getEntityTitle(): string {

            if (!this.isEditMode()) {
                return "How did you manage to open this dialog in new record mode?";
            }
            else {
                var entityType = super.getEntitySingular();
                let name = this.getEntityNameFieldValue() || "";
                return 'View ' + entityType + " (" + name + ")";
            }
        }

        protected updateTitle(): void {
            super.updateTitle();
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();
            //buttons.shift();
            //buttons.shift();
            //buttons.shift();

            buttons.push({
                title: 'To Ticket',
                icon: 'fa-share-square text-yellow',
                hint: 'Move to Ticket',
                onClick: () => {
                    MailInboxDetailsService.MoveToTicket({ //Disable if not applicable
                        Id: this.get_entityId()
                    },
                        response => {
                            if (response.Id > 0) {                               
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