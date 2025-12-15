namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class TeleCallingDialog extends DialogBase<TeleCallingRow, any> {
        protected getFormKey() { return TeleCallingForm.formKey; }
        protected getIdProperty() { return TeleCallingRow.idProperty; }
        protected getLocalTextPrefix() { return TeleCallingRow.localTextPrefix; }
        protected getNameProperty() { return TeleCallingRow.nameProperty; }
        protected getService() { return TeleCallingService.baseUrl; }
        protected getDeletePermission() { return TeleCallingRow.deletePermission; }
        protected getInsertPermission() { return TeleCallingRow.insertPermission; }
        protected getUpdatePermission() { return TeleCallingRow.updatePermission; }

        protected form = new TeleCallingForm(this.idPrefix);


        private followupsGrid: TeleCallingFollowupsGrid;
        private appointmentsGrid: TeleCallingAppointmentsGrid;

        constructor() {
            super();

            this.followupsGrid = new TeleCallingFollowupsGrid(this.byId('FollowupsGrid'));
            this.appointmentsGrid = new TeleCallingAppointmentsGrid(this.byId('AppointmentsGrid'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            this.form.Details.getGridField().toggle(false);
            this.form.AssignedTo.getGridField().toggle(false);

            this.form.ContactsId.changeSelect2(e => {
                this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Phone;
            });

            this.form.Status.change(e => {
                if (this.form.Status.value == "2") {
                    this.form.Details.getGridField().toggle(true);
                    this.form.AssignedTo.getGridField().toggle(true);
                }
                else {
                    this.form.Details.getGridField().toggle(false);
                    this.form.AssignedTo.getGridField().toggle(false);
                }
            });

        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'SMS Customer',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send appointment SMS to customer',
                onClick: () => {
                    TeleCallingService.SendSMS({
                        Id: Q.toId(this.entityId),
                        SMSType: "Customer"
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                },
                separator: true
            });

            buttons.push({
                title: 'SMS Executive',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send appointment SMS to executive',
                onClick: () => {
                    TeleCallingService.SendSMS({
                        Id: Q.toId(this.entityId),
                        SMSType: "Executive"
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            buttons.push({
                title: 'Email Customer',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send appointment Email to customer',
                onClick: () => {
                    TeleCallingService.SendMail({
                        Id: Q.toId(this.entityId),
                        MailType: "Customer"
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            buttons.push({
                title: 'Email Executive',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send appointment Email to executive',
                onClick: () => {
                    TeleCallingService.SendMail({
                        Id: Q.toId(this.entityId),
                        MailType: "Executive"
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            buttons.push({
                title: 'Executive Reminder',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send appointment reminder SMS to executive',
                onClick: () => {
                    if (this.form.AppointmentDate.value != undefined) {
                        TeleCallingService.SendSMS({
                            Id: Q.toId(this.entityId),
                            SMSType: "Executive Reminder"
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );
                    }
                    else {
                        Q.alert('Appointment date not selected');
                    }
                }
            });

            buttons.push({
                title: 'Customer Reminder',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send appointment reminder SMS to Customer',
                onClick: () => {
                    if (this.form.AppointmentDate.value != undefined) {
                        TeleCallingService.SendSMS({
                            Id: Q.toId(this.entityId),
                            SMSType: "Customer Reminder"
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );
                    }
                    else {
                        Q.alert('Appointment date not selected');
                    }
                }
            });


            if (Authorization.hasPermission("IVR:Click To Call")) {
                buttons.push({
                    title: 'Call',
                    icon: 'fa-phone text-blue',
                    onClick: () => {
                        if (Q.toId(this.form.ContactsId.value) != null) {
                            var phn = this.form.ContactsPhone.value;

                            var dialog = new ThirdParty.IVRCallDialog({
                                CustomerNumber: phn,
                                ServiceURL: this.getService()
                            });

                            dialog.dialogOpen();
                        }
                        else
                            Q.notifyError("Contact Invalid!!");
                    }
                });
            }

            buttons.push({
                title: 'To Enquiry',
                cssClass: 'send-button',
                icon: 'fa-share-square text-blue',
                hint: 'Move to Enquiry',
                onClick: () => {
                    TeleCallingService.MoveToEnquiry({ //Disable if not applicable
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

        onDialogOpen() {
            super.onDialogOpen();
            if (this.form.RepresentativeId.value < "1") {
                this.form.RepresentativeId.value = Authorization.userDefinition.UserId.toString();
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].AppointmentInTeleCalling != true) {
                Serenity.TabsExtensions.toggle(this.tabs, 'Appointments', false);
            }
        }

        loadEntity(entity: TeleCallingRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Appointments', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.TeleCallingId = entity.Id.toString();
                this.appointmentsGrid.TeleCallingId = entity.Id.toString();
            }
        }
    }
}