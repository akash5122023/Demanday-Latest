
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class RpPaymentDetailsDialog extends Serenity.EntityDialog<RpPaymentDetailsRow, any> {
        protected getFormKey() { return RpPaymentDetailsForm.formKey; }
        protected getIdProperty() { return RpPaymentDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return RpPaymentDetailsRow.localTextPrefix; }
        protected getNameProperty() { return RpPaymentDetailsRow.nameProperty; }
        protected getService() { return RpPaymentDetailsService.baseUrl; }
        protected getDeletePermission() { return RpPaymentDetailsRow.deletePermission; }
        protected getInsertPermission() { return RpPaymentDetailsRow.insertPermission; }
        protected getUpdatePermission() { return RpPaymentDetailsRow.updatePermission; }

        protected form = new RpPaymentDetailsForm(this.idPrefix);

        protected updateInterface(): void {
            super.updateInterface();
            //Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
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
                title: 'To Enquiry',
                icon: 'fa-share-square text-yellow',
                hint: 'Move to Enquiry',
                onClick: () => {
                    RpPaymentDetailsService.MoveToEnquiry({ //Disable if not applicable
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