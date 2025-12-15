
namespace AdvanceCRM.ThirdParty{

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class SIndiaMartDetailsDialog extends Serenity.EntityDialog<SIndiaMartDetailsRow, any> {
        protected getFormKey() { return SIndiaMartDetailsForm.formKey; }
        protected getIdProperty() { return SIndiaMartDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return SIndiaMartDetailsRow.localTextPrefix; }
        protected getNameProperty() { return SIndiaMartDetailsRow.nameProperty; }
        protected getService() { return SIndiaMartDetailsService.baseUrl; }
        protected getDeletePermission() { return SIndiaMartDetailsRow.deletePermission; }
        protected getInsertPermission() { return SIndiaMartDetailsRow.insertPermission; }
        protected getUpdatePermission() { return SIndiaMartDetailsRow.updatePermission; }

        protected form = new SIndiaMartDetailsForm(this.idPrefix);

        protected updateInterface(): void {
            super.updateInterface();
            Serenity.EditorUtils.setReadonly(this.element.find('.editor'), false);
            this.element.find('sup').hide();
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
                title: 'To Enquiry',
                icon: 'fa-share-square text-yellow',
                hint: 'Move to Enquiry',
                onClick: () => {
                    SIndiaMartDetailsService.MoveToEnquiry({ //Disable if not applicable
                        Id: this.get_entityId()
                    },
                        response => {
                            if (response.Id > 0) {
                                Q.reloadLookup(Contacts.ContactsRow.lookupKey);
                                Q.reloadLookup(Contacts.SubContactsRow.lookupKey);
                                Q.notifyInfo(response.Status);

                                new Enquiry.EnquiryDialog().loadByIdAndOpenDialog(response.Id);
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