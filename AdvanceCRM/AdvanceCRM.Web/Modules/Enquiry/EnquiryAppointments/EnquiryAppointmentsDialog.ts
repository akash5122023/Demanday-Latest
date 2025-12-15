
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryAppointmentsDialog extends DialogBase<EnquiryAppointmentsRow, any> {
        protected getFormKey() { return EnquiryAppointmentsForm.formKey; }
        protected getIdProperty() { return EnquiryAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryAppointmentsRow.nameProperty; }
        protected getService() { return EnquiryAppointmentsService.baseUrl; }
        protected getDeletePermission() { return EnquiryAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryAppointmentsRow.updatePermission; }

        protected form = new EnquiryAppointmentsForm(this.idPrefix);

        constructor() {
            super();

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "1") {
                    this.form.MinutesOfMeeting.getGridField().toggle(false);
                    this.form.Reason.getGridField().toggle(false);
                }
                else if (this.form.Status.value == "2") {
                    this.form.MinutesOfMeeting.getGridField().toggle(true);
                    this.form.Reason.getGridField().toggle(false);
                }
                else {
                    this.form.MinutesOfMeeting.getGridField().toggle(false);
                    this.form.Reason.getGridField().toggle(true);
                }
            });
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send mail to customer',
                onClick: () => {
                    EnquiryAppointmentsService.SendMail({
                        Id: this.get_entityId()
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                },
                separator: true
            });

            buttons.push({
                title: 'SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send SMS to customer',
                onClick: () => {
                    EnquiryAppointmentsService.SendSMS({
                        Id: this.get_entityId()
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.RepresentativeId.value < "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.Status.value == "1") {
                this.form.MinutesOfMeeting.getGridField().toggle(false);
                this.form.Reason.getGridField().toggle(false);
            }
            else if (this.form.Status.value == "2") {
                this.form.MinutesOfMeeting.getGridField().toggle(true);
                this.form.Reason.getGridField().toggle(false);
            }
            else {
                this.form.MinutesOfMeeting.getGridField().toggle(false);
                this.form.Reason.getGridField().toggle(true);
            }
        }
    }
}