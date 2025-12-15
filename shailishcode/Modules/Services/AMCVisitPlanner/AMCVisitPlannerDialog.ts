
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class AMCVisitPlannerDialog extends DialogBase<AMCVisitPlannerRow, any> {
        protected getFormKey() { return AMCVisitPlannerForm.formKey; }
        protected getIdProperty() { return AMCVisitPlannerRow.idProperty; }
        protected getLocalTextPrefix() { return AMCVisitPlannerRow.localTextPrefix; }
        protected getNameProperty() { return AMCVisitPlannerRow.nameProperty; }
        protected getService() { return AMCVisitPlannerService.baseUrl; }
        protected getDeletePermission() { return AMCVisitPlannerRow.deletePermission; }
        protected getInsertPermission() { return AMCVisitPlannerRow.insertPermission; }
        protected getUpdatePermission() { return AMCVisitPlannerRow.updatePermission; }

        protected form = new AMCVisitPlannerForm(this.idPrefix);

        constructor() {
            super();

            this.form.CompletionDate.getGridField().toggle(false);
            this.form.VisitDetails.getGridField().toggle(false);
            this.form.Attachment.getGridField().toggle(false);

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "2") {
                    this.form.CompletionDate.getGridField().toggle(true);
                    this.form.CompletionDate.value = "now";
                    this.form.VisitDetails.getGridField().toggle(true);
                    this.form.Attachment.getGridField().toggle(true);
                }
                else {
                    this.form.CompletionDate.getGridField().toggle(false);
                    this.form.VisitDetails.getGridField().toggle(false);
                    this.form.CompletionDate.value = null;
                    this.form.Attachment.getGridField().toggle(false);
                }
            });

        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'Visit SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send Visit SMS to customer',
                onClick: () => {
                    AMCVisitPlannerService.SendVisitSMS({
                        Id: Q.toId(this.entity.Id)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
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

            if (this.form.Status.value == "2") {
                this.form.CompletionDate.getGridField().toggle(true);
                this.form.VisitDetails.getGridField().toggle(true);
                this.form.Attachment.getGridField().toggle(true);
            }
            else {
                this.form.CompletionDate.getGridField().toggle(false);
                this.form.VisitDetails.getGridField().toggle(false);
                this.form.Attachment.getGridField().toggle(false);
            }

            Serenity.EditorUtils.setReadonly(this.form.RepresentativeId.element, true);
        }
    }
}