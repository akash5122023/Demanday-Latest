
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryFollowupsDialog extends DialogBase<EnquiryFollowupsRow, any> {
        protected getFormKey() { return EnquiryFollowupsForm.formKey; }
        protected getIdProperty() { return EnquiryFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryFollowupsRow.nameProperty; }
        protected getService() { return EnquiryFollowupsService.baseUrl; }
        protected getDeletePermission() { return EnquiryFollowupsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryFollowupsRow.updatePermission; }

        protected form = new EnquiryFollowupsForm(this.idPrefix);

        constructor() {
            super();

            //this.form.ContactsId.changeSelect2(e => {

            //this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[Enquiry.EnquiryForm.ContactsId.value].Phone;
            //    this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Address;

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "2") {
                    this.form.ClosingDate.getGridField().toggle(true)
                    this.form.ClosingDate.valueAsDate = new Date();
                }
                else {
                    this.form.ClosingDate.getGridField().toggle(false)
                }
            });
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.RepresentativeId.value < "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.Status.value != '2') {
                this.form.ClosingDate.getGridField().toggle(false);
            }
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'SMS Reminder',
                cssClass: 'send-button', icon: 'fa-comments-o text-yellow',
                hint: 'Schedule SMS reminder',
                onClick: () => {
                    EnquiryFollowupsService.SendSMSReminder({
                        Id: Q.toId(this.entity.Id.toString())
                    },
                        response => {
                            Q.notifyInfo(response.Status.toString());
                        }
                    );
                },
                separator: true
            });

            buttons.push(
                {
                    title: 'SMS',
                    cssClass: 'send-button', icon: 'fa-comments-o text-green',
                    hint: 'Send SMS to customer',
                    onClick: () => {
                        //if (Q.toId(this.form.EnquiryContactsId.value) != null) {
                        //    if(Q.toId)
                        //    var phn = this.form.en.value;
                        //    if (this.form.ContactsContactType.value != 1) {
                        //        if (Q.toId(this.form.ContactPersonId.value)) {
                        //            phn = this.form.ContactPersonPhone.value;
                        //        }
                        //    }
                            
                        if (this.form.ContactPhone.value != "")
                            var num = this.form.ContactPhone.value;
                        else
                            num = this.form.ContactPersonPhone.value;

                        var dialog = new Common.QuickSMSDialog({
                            Number: num
                        });

                        dialog.element.on('dialogclose', () => {
                            dialog = null;
                        });

                        dialog.dialogOpen();
                    },
                    separator: true
                }
            );

            buttons.push(
                {
                    title: 'Mail',
                    cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                    hint: 'Send mail to customer',
                    onClick: () => {
                        var email = "";
                        if (this.form.ContactEmail.value != "")
                            email = this.form.ContactEmail.value;
                        else
                            email = this.form.ContactPersonEmail.value;

                        var dialog = new Common.QuickEmailDialog({
                            Email: email
                        });

                        dialog.element.on('dialogclose', () => {
                            dialog = null;
                        });

                        dialog.dialogOpen();
                    }
                }
            );

            return buttons;
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }
    }
}