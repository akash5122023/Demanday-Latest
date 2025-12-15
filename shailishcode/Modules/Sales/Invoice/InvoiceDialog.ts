
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class InvoiceDialog extends DialogBase<InvoiceRow, any> {
        protected getFormKey() { return InvoiceForm.formKey; }
        protected getIdProperty() { return InvoiceRow.idProperty; }
        protected getLocalTextPrefix() { return InvoiceRow.localTextPrefix; }
        protected getNameProperty() { return InvoiceRow.nameProperty; }
        protected getService() { return InvoiceService.baseUrl; }
        protected getDeletePermission() { return InvoiceRow.deletePermission; }
        protected getInsertPermission() { return InvoiceRow.insertPermission; }
        protected getUpdatePermission() { return InvoiceRow.updatePermission; }

        protected form = new InvoiceForm(this.idPrefix);

        private followupsGrid: InvoiceFollowupsGrid;
        private appointmentsGrid: InvoiceAppointmentsGrid;

        constructor() {
            super();

            this.followupsGrid = new InvoiceFollowupsGrid(this.byId('FollowupsGrid'));
            this.appointmentsGrid = new InvoiceAppointmentsGrid(this.byId('AppointmentsGrid'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            this.form.ContactPersonId.getGridField().toggle(false);
            this.form.ContactPersonPhone.getGridField().toggle(false);
            this.form.ContactPersonProject.getGridField().toggle(false);
            this.form.ContactPersonAddress.getGridField().toggle(false);

            this.form.ContactsId.changeSelect2(e => {
                this.form.Products.ContactsId = Q.toId(this.form.ContactsId.value);
                this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Phone;
                this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Address;
                var isOrganisation = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization;

                this.form.ContactPersonId.getGridField().toggle(isOrganisation);
                this.form.ContactPersonPhone.getGridField().toggle(isOrganisation);

                if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions == true) {
                    this.form.ContactsAddress.getGridField().toggle(!isOrganisation);
                    this.form.ContactPersonAddress.getGridField().toggle(isOrganisation);
                }

                if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts == true) {
                    this.form.ContactPersonProject.getGridField().toggle(isOrganisation);
                }
            });

            this.form.ContactPersonId.changeSelect2(e => {

                var CId = Q.toId(this.form.ContactPersonId.value);

                if (CId > 0) {
                    this.form.ContactPersonPhone.value = Contacts.SubContactsRow.getLookup().itemById[CId].Phone;
                    this.form.ContactPersonAddress.value = Contacts.SubContactsRow.getLookup().itemById[CId].Address;
                    this.form.ContactPersonProject.value = Contacts.SubContactsRow.getLookup().itemById[CId].Project;
                }
                else {
                    this.form.ContactPersonPhone.value = "";
                    this.form.ContactPersonAddress.value = "";
                    this.form.ContactPersonProject.value = "";
                }

            });

            this.form.ShippingAddress.getGridField().toggle(false);

            this.form.OtherAddress.change(e => {
                if (this.form.OtherAddress.value == true) {
                    this.form.ShippingAddress.getGridField().toggle(true);
                }
                else {
                    this.form.ShippingAddress.getGridField().toggle(false);
                }
            });

            this.form.CurrencyConversion.change(e => {
                if (this.form.CurrencyConversion.value == true) {
                    this.form.Conversion.getGridField().toggle(true);
                    this.form.FromCurrency.getGridField().toggle(true);
                    this.form.ToCurrency.getGridField().toggle(true);
                }
                else {
                    this.form.Conversion.getGridField().toggle(false);
                    this.form.FromCurrency.getGridField().toggle(false);
                    this.form.ToCurrency.getGridField().toggle(false);
                }
            });

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "2") {
                    this.form.ClosingDate.getGridField().toggle(true)
                    this.form.ClosingDate.value = 'now';
                }
                else {
                    this.form.ClosingDate.getGridField().toggle(false)
                    this.form.ClosingDate.value = null;
                }
            });
        }

        protected updateInterface() {
            super.updateInterface();
            if (Authorization.hasPermission("Proforma:Clone")) {
                this.cloneButton.toggle(this.isEditMode());
            }
            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            //if (Authorization.hasPermission("Proforma:Move to Clone")) {
            //    buttons.push({
            //        title: 'Clone',
            //        icon: 'fa fa-share-square text-red',
            //        hint: 'Clone Proforma',
            //        onClick: () => {
            //            InvoiceService.MoveToClone({
            //                Id: this.get_entityId()
            //            },
            //                response => {
            //                    Q.notifyInfo(response.Status);
            //                    new Sales.InvoiceDialog().loadByIdAndOpenDialog(response.Id);
            //                }
            //            );
            //        }
            //    });
            //}

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Invoice.PrintInvoice',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'Invoice.PrintInvoice',
                getParams: () => ({ Id: this.get_entityId(), ModType: "Invoice" }), //ModType Invoice, Invoice
                download: true //Set true if have to send mail
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'BizWhatsApp',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                reportKey: 'Invoice.PrintInvoice',
                getParams: () => ({ Id: this.get_entityId(), ModType: "WAInvoice" }), //ModType Invoice, Invoice
                download: true //Set true if have to send mail
            }));

            //buttons.push(
            //    {
            //        title: 'QuickWati',
            //        cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //        hint: 'Send thankyou Whatsapp to customer',
            //        onClick: () => {
            //            var num = "";
            //            if (this.form.ContactsWhatsapp.value != "")
            //                num = this.form.ContactsWhatsapp.value;
            //            else
            //                num = this.form.ContactsPhone.value;

            //            var dialog = new Common.WatiDialog({
            //                Number: num
            //            });

            //            dialog.element.on('dialogclose', () => {
            //                dialog = null;
            //            });

            //            dialog.dialogOpen();
            //        },
            //        separator: true
            //    }
            //);


            if (Authorization.hasPermission("IVR:Click To Call")) {
                buttons.push({
                    title: 'Call',
                    icon: 'fa-phone text-blue',
                    onClick: () => {
                        if (Q.toId(this.form.ContactsId.value) != null) {
                            var phn = this.form.ContactsPhone.value;
                            if (this.form.ContactsContactType.value != 1) {
                                if (Q.toId(this.form.ContactPersonId.value)) {
                                    phn = this.form.ContactPersonPhone.value;
                                }
                            }

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

            //buttons.push(
            //    {
            //        title: 'BizWhatsApp',
            //        cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //        hint: 'Bizplus Whatsapp Sender',
            //        onClick: () => {
            //            var num = "";
            //            var name = "";
            //            if (this.form.ContactsWhatsapp.value != "") {
            //                num = this.form.ContactsWhatsapp.value;
            //                name = this.form.ContactsName.value;
            //            }
            //            else {
            //                num = this.form.ContactsPhone.value;
            //                name = this.form.ContactsName.value;
            //            }

            //            const contacts = [{ number: num, name: name }];

            //            var dialog = new Common.IntractWaDialog({ Contacts: contacts });


            //            dialog.element.on('dialogclose', () => {
            //                dialog = null;
            //            });

            //            dialog.dialogOpen();
            //        },
            //        separator: true
            //    }
            //);

            buttons.push({
                title: 'Whatsapp',
                icon: 'fa-whatsapp text-green',
                hint: 'Send WhatsApp Message',
                onClick: () => {
                    if (Q.toId(this.form.ContactsId.value) != null) {
                        var name = this.form.ContactsName.value;
                        var phn = this.form.ContactsWhatsapp.value;
                        if (this.form.ContactsContactType.value != 1) {
                            if (Q.toId(this.form.ContactPersonId.value)) {
                                name = this.form.ContactPersonName.value;
                                phn = this.form.ContactPersonWhatsapp.value;
                            }
                        }

                        var template = Q.tryFirst(Template.InvoiceTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
                }
            });


            //Old type
            //buttons.push({

            //    title: 'Mail',
            //    cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
            //    hint: 'Send Invoice over mail',
            //    onClick: () => {
            //        InvoiceService.SendMail({
            //            Id: this.get_entityId()
            //        },
            //            response => {
            //                Q.notifyInfo(response.Status);
            //            }
            //        );
            //    }
            //});

            buttons.push({

                title: 'SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send thankyou SMS to customer',
                onClick: () => {
                    InvoiceService.SendSMS({
                        Id: Q.toId(this.get_entityId)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    Q.alert('SMS for Proforma will be available soon');
                }
            });

            //  buttons.push({
            //    title: 'BizWA',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Send thankyou Whatsapp to customer',
            //    onClick: () => {
            //        InvoiceService.SendWati({
            //            Id: Q.toId(this.entityId)
            //        },
            //            response => {
            //                Q.notifyInfo(response.Status);
            //            }
            //        );
            //    }
            //});

            if (Authorization.hasPermission("Proforma:Move to Invoice")) {
                buttons.push({
                    title: 'To Invoice',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move to Inovice',
                    onClick: () => {
                        InvoiceService.MoveToInvoice({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "Sales"
                        },
                            response => {
                                if (response.Id > 0) {
                                    Q.notifyInfo(response.Status);
                                    new SalesDialog().loadByIdAndOpenDialog(response.Id);
                                }
                                else
                                    Q.notifyError(response.Status)
                            }
                        );
                    }
                });
            }


            if (Authorization.hasPermission("Invoice:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'approve-button',
                    icon: 'fa-check-circle text-green',
                    hint: 'Approve this Invoice',
                    onClick: () => {
                        InvoiceService.Approve({
                            Id: Q.toId(this.entityId)
                        },
                            response => {
                                if (response.Status == "Approved") {
                                    Q.notifySuccess(response.Status);
                                    Serenity.SubDialogHelper.triggerDataChange(this);
                                }
                                else {
                                    Q.notifyError(response.Status);
                                }
                            });
                    },
                    separator: true
                });
            }


            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();

            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].AppointmentInSales != true) {
                Serenity.TabsExtensions.toggle(this.tabs, 'Appointments', false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MultiCurrency != true) {
                this.form.CurrencyConversion.getGridField().toggle(false);
                this.form.Conversion.getGridField().toggle(false);
                this.form.FromCurrency.getGridField().toggle(false);
                this.form.ToCurrency.getGridField().toggle(false);
            }

            if (this.form.CurrencyConversion.value == true) {
                this.form.Conversion.getGridField().toggle(true);
                this.form.FromCurrency.getGridField().toggle(true);
                this.form.ToCurrency.getGridField().toggle(true);
            }
            else {
                this.form.Conversion.getGridField().toggle(false);
                this.form.FromCurrency.getGridField().toggle(false);
                this.form.ToCurrency.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DealerInInvoice != true) {
                this.form.DealerId.getGridField().toggle(false);
            }
            else
            {
                this.form.DealerId.getGridField().toggle(true);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                this.form.ContactPersonProject.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].InvEditNo == true) {
                Serenity.EditorUtils.setReadonly(this.form.InvoiceN.element, false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAdditionalCharges != true) {
                this.form.ChargesList.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAdditionalConcessions != true) {
                this.form.ConcessionList.getGridField().toggle(false);
            }

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.OwnerId.value > "1") {
                this.form.CompanyId.value = Q.toId(Authorization.userDefinition.CompanyId);
            }


            if (this.form.Status.value != '2') {
                this.form.ClosingDate.getGridField().toggle(false);
            }

            if (this.form.OtherAddress.value == true) {
                this.form.ShippingAddress.getGridField().toggle(true);
            }
            else {
                this.form.ShippingAddress.getGridField().toggle(false);
            }

            if (this.form.TermsList.value == '') {
                this.form.TermsList.value = Q.tryFirst(Template.InvoiceTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).TermsConditions;
            }

            if (this.form.ContactsId.value >= "1") {
                if (Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization) {
                    this.form.ContactPersonId.getGridField().toggle(true);
                    this.form.ContactPersonPhone.getGridField().toggle(true);
                    this.form.ContactPersonProject.getGridField().toggle(true);
                    this.form.ContactPersonAddress.getGridField().toggle(true);

                    this.form.ContactsPhone.getGridField().toggle(false);
                    this.form.ContactsAddress.getGridField().toggle(false);
                }
            }

            
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions != true) {
                this.form.ContactsAddress.getGridField().toggle(false);
                this.form.ContactPersonAddress.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions != true) {
                this.form.ContactsAddress.getGridField().toggle(false);
                this.form.ContactPersonAddress.getGridField().toggle(false);
            }

            this.form.ContactsPhone.change(e => {
                var code = Q.text(this.form.ContactsPhone.value);
                if (code.trim() != "") {
                    this.form.ContactsId.value = Q.tryFirst(Contacts.ContactsRow.getLookup().items, x => x.Phone == code).Id.toString();
                    this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Address;
                    var isOrganisation = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization;

                    this.form.ContactPersonId.getGridField().toggle(isOrganisation);
                    this.form.ContactPersonPhone.getGridField().toggle(isOrganisation);

                    if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions == true) {
                        this.form.ContactsAddress.getGridField().toggle(!isOrganisation);
                        this.form.ContactPersonAddress.getGridField().toggle(isOrganisation);
                    }

                    if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts == true) {
                        this.form.ContactPersonProject.getGridField().toggle(isOrganisation);
                    }
                }
            });

            if (this.form.InvoiceNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                InvoiceService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.InvoiceN.value = Q.trimToNull(response.SerialN);
                    this.form.InvoiceNo.value = Q.toId(response.Serial);
                });
            }
        }

        loadEntity(entity: InvoiceRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Appointments', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.InvoiceId = entity.Id.toString();
                this.appointmentsGrid.InvoiceId = entity.Id.toString();
            }
        }

        getCloningEntity() {
            var clone = super.getCloningEntity();
            delete clone.Timeline;
            delete clone.NoteList;
            return clone;
        }
    }
}