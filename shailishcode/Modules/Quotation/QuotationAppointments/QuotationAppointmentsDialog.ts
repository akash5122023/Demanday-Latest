
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    export class QuotationAppointmentsDialog extends DialogBase<QuotationAppointmentsRow, any> {
        protected getFormKey() { return QuotationAppointmentsForm.formKey; }
        protected getIdProperty() { return QuotationAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return QuotationAppointmentsRow.nameProperty; }
        protected getService() { return QuotationAppointmentsService.baseUrl; }
        protected getDeletePermission() { return QuotationAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return QuotationAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return QuotationAppointmentsRow.updatePermission; }

        protected form = new QuotationAppointmentsForm(this.idPrefix);

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
                    QuotationAppointmentsService.SendMail({
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
                    QuotationAppointmentsService.SendSMS({
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