
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingAppointmentsDialog extends DialogBase<TeleCallingAppointmentsRow, any> {
        protected getFormKey() { return TeleCallingAppointmentsForm.formKey; }
        protected getIdProperty() { return TeleCallingAppointmentsRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingAppointmentsRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingAppointmentsRow.nameProperty; }
        protected getService() { return TeleCallingAppointmentsService.baseUrl; }
        protected getDeletePermission() { return TeleCallingAppointmentsRow.deletePermission; }
        protected getInsertPermission() { return TeleCallingAppointmentsRow.insertPermission; }
        protected getUpdatePermission() { return TeleCallingAppointmentsRow.updatePermission; }

        protected form = new TeleCallingAppointmentsForm(this.idPrefix);

        constructor() {
            super();

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "1") {
                    this.form.MinutesOfMeeting.getGridField().toggle(false);
                    this.form.Reason.getGridField().toggle(false);
                }
                else if (this.form.Status.value == "2") {
                    this.form.MinutesOfMeeting.getGridField().toggle(true);
                    this.form.Reason.getGridField().toggle(true);
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
                    TeleCallingAppointmentsService.SendMail({
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
                    TeleCallingAppointmentsService.SendSMS({
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