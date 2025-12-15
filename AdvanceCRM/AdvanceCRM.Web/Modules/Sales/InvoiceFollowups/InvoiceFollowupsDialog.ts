
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InvoiceFollowupsDialog extends DialogBase<InvoiceFollowupsRow, any> {
        protected getFormKey() { return InvoiceFollowupsForm.formKey; }
        protected getIdProperty() { return InvoiceFollowupsRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceFollowupsRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceFollowupsRow.nameProperty; }
        protected getService() { return InvoiceFollowupsService.baseUrl; }
        protected getDeletePermission() { return InvoiceFollowupsRow.deletePermission; }
        protected getInsertPermission() { return InvoiceFollowupsRow.insertPermission; }
        protected getUpdatePermission() { return InvoiceFollowupsRow.updatePermission; }

        protected form = new InvoiceFollowupsForm(this.idPrefix);

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
                    InvoiceFollowupsService.SendSMSReminder({
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
                        var num = "";
                        if (this.form.ContactPersonPhone.value != "")
                            num = this.form.ContactPersonPhone.value;
                        else
                            num = this.form.ContactPhone.value;

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
                        if (this.form.ContactPersonEmail.value != "")
                            email = this.form.ContactPersonEmail.value;
                        else
                            email = this.form.ContactEmail.value;

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