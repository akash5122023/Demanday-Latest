
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class KnowlarityDetailsDialog extends Serenity.EntityDialog<KnowlarityDetailsRow, any> {
        protected getFormKey() { return KnowlarityDetailsForm.formKey; }
        protected getIdProperty() { return KnowlarityDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return KnowlarityDetailsRow.localTextPrefix; }
        protected getNameProperty() { return KnowlarityDetailsRow.nameProperty; }
        protected getService() { return KnowlarityDetailsService.baseUrl; }
        protected getDeletePermission() { return KnowlarityDetailsRow.deletePermission; }
        protected getInsertPermission() { return KnowlarityDetailsRow.insertPermission; }
        protected getUpdatePermission() { return KnowlarityDetailsRow.updatePermission; }

        protected form = new KnowlarityDetailsForm(this.idPrefix);

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();
            //buttons.shift();
            //buttons.shift();
            //buttons.shift();

            buttons.push({
                title: 'To CMS',
                icon: 'fa-share-square text-yellow',
                hint: 'Move to CMS',
                onClick: () => {
                    KnowlarityDetailsService.MoveToCMS({ //Disable if not applicable
                        Id: this.get_entityId()
                    },
                        response => {
                            if (response.Id > 0) {
                                Q.reloadLookup(Contacts.ContactsRow.lookupKey);
                                Q.reloadLookup(Contacts.SubContactsRow.lookupKey);
                                Q.notifyInfo(response.Status);

                                new Services.CMSDialog().loadByIdAndOpenDialog(response.Id);
                            }
                            else
                                Q.notifyError(response.Status)
                        }
                    );
                }
            });

            buttons.push({
                title: 'To Enquiry',
                icon: 'fa-share-square text-blue',
                hint: 'Move to Enquiry',
                onClick: () => {
                    KnowlarityDetailsService.MoveToEnquiry({ //Disable if not applicable
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

            buttons.push({
                title: 'To Play',
                icon: 'fa-share-square text-yellow',
                hint: 'Play Recording',
                onClick: () => {
                    KnowlarityDetailsService.play({
                        Id: this.get_entityId()
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                },
                separator: true
            });


            return buttons;
        }

    }
}