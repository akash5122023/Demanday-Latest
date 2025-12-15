
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class AMCDialog extends DialogBase<AMCRow, any> {
        protected getFormKey() { return AMCForm.formKey; }
        protected getIdProperty() { return AMCRow.idProperty; }
        protected getLocalTextPrefix() { return AMCRow.localTextPrefix; }
        protected getNameProperty() { return AMCRow.nameProperty; }
        protected getService() { return AMCService.baseUrl; }
        protected getDeletePermission() { return AMCRow.deletePermission; }
        protected getInsertPermission() { return AMCRow.insertPermission; }
        protected getUpdatePermission() { return AMCRow.updatePermission; }

        protected form = new AMCForm(this.idPrefix);

        private visitGrid: AMCVisitPlannerGrid;

        constructor() {
            super();

            this.visitGrid = new AMCVisitPlannerGrid(this.byId('VisitsGrid'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'AMC.PrintAMC',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'AMC.PrintAMC',
                getParams: () => ({ Id: this.get_entityId(), ModType: "AMC" }), //ModType Quotation, Invoice, AMC
                download: true //Set true if have to send mail
            }));

            if (Authorization.hasPermission("IVR:Click To Call")) {
                buttons.push({
                    title: 'Call',
                    icon: 'fa-phone text-blue',
                    onClick: () => {
                        if (Q.toId(this.form.ContactsId.value) != null) {
                            var phn = this.form.ContactsPhone.value;
                            //if (this.form.ContactsContactType.value != 1) {
                            //    if (Q.toId(this.form.ContactPersonId.value)) {
                            //        phn = this.form.ContactPersonPhone.value;
                            //    }
                            //}

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

            buttons.push(
                {
                    title: 'BizWhatsApp',
                    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                    hint: 'Bizplus Whatsapp Sender',
                    onClick: () => {
                        var num = "";
                        var name = "";
                        if (this.form.ContactsWhatsapp.value != "") {
                            num = this.form.ContactsWhatsapp.value;
                            name = this.form.ContactsName.value;
                        }
                        else {
                            num = this.form.ContactsPhone.value;
                            name = this.form.ContactsName.value;
                        }

                        const contacts = [{ number: num, name: name }];

                        var dialog = new Common.IntractWaDialog({ Contacts: contacts });


                        dialog.element.on('dialogclose', () => {
                            dialog = null;
                        });

                        dialog.dialogOpen();
                    },
                    separator: true
                }
            );
            buttons.push({
                title: 'Whatsapp',
                icon: 'fa-whatsapp text-green',
                hint: 'Send WhatsApp Message',
                onClick: () => {
                    if (Q.toId(this.form.ContactsId.value) != null) {
                        var name = this.form.ContactsName.value;
                        var phn = this.form.ContactsWhatsapp.value;

                        var template = Template.AMCTemplateRow.getLookup().itemById[1].SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
                }
            });

            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.TermsList.value.trim() == "") {
                this.form.TermsList.value = Template.AMCTemplateRow.getLookup().itemById[1].TermsConditions;
            }


            if (this.form.AMCNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                AMCService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.AMCNo.value = Q.toId(response.Serial);
                });
            }

        }

        loadEntity(entity: AMCRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Visits', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.visitGrid.AMCId = entity.Id.toString();
            }
        }
    }
}

