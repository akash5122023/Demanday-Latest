
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingFollowupsDialog extends DialogBase<TeleCallingFollowupsRow, any> {
        protected getFormKey() { return TeleCallingFollowupsForm.formKey; }
        protected getIdProperty() { return TeleCallingFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingFollowupsRow.nameProperty; }
        protected getService() { return TeleCallingFollowupsService.baseUrl; }
        protected getDeletePermission() { return TeleCallingFollowupsRow.deletePermission; }
        protected getInsertPermission() { return TeleCallingFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return TeleCallingFollowupsRow.updatePermission; }

        protected form = new TeleCallingFollowupsForm(this.idPrefix);

        constructor() {
            super();
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

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'SMS Reminder',
                cssClass: 'send-button', icon: 'fa-comments-o text-yellow',
                hint: 'Schedule SMS reminder',
                onClick: () => {
                    TeleCallingFollowupsService.SendSMSReminder({
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
                        var num = this.form.ContactPhone.value;

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
                        var email = this.form.ContactEmail.value;

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

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.RepresentativeId.value < "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.Status.value != '2') {
                this.form.ClosingDate.getGridField().toggle(false);
            }
        }
    }
}