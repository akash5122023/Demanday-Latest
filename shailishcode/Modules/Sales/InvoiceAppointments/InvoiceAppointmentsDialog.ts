
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceAppointmentsDialog extends DialogBase<InvoiceAppointmentsRow, any> {
        protected getFormKey() { return InvoiceAppointmentsForm.formKey; }
        protected getIdProperty() { return InvoiceAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceAppointmentsRow.nameProperty; }
        protected getService() { return InvoiceAppointmentsService.baseUrl; }
        protected getDeletePermission() { return InvoiceAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return InvoiceAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return InvoiceAppointmentsRow.updatePermission; }

        protected form = new InvoiceAppointmentsForm(this.idPrefix);

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
                    InvoiceAppointmentsService.SendMail({
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
                    InvoiceAppointmentsService.SendSMS({
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