
namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class QuotationDialog extends DialogBase<QuotationRow, any> {
        protected getFormKey() { return QuotationForm.formKey; }
        protected getIdProperty() { return QuotationRow.idProperty; }
        protected getLocalTextPrefix() { return QuotationRow.localTextPrefix; }
        protected getNameProperty() { return QuotationRow.nameProperty; }
        protected getService() { return QuotationService.baseUrl; }
        protected getDeletePermission() { return QuotationRow.deletePermission; }
        protected getInsertPermission() { return QuotationRow.insertPermission; }
        protected getUpdatePermission() { return QuotationRow.updatePermission; }

        protected form = new QuotationForm(this.idPrefix);

        private followupsGrid: QuotationFollowupsGrid;
        private appointmentsGrid: QuotationAppointmentsGrid;

        constructor() {
            super();

            this.followupsGrid = new QuotationFollowupsGrid(this.byId('FollowupsGrid'));
            this.appointmentsGrid = new QuotationAppointmentsGrid(this.byId('AppointmentsGrid'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            this.form.ReferenceName.getGridField().toggle(false)
            this.form.ReferencePhone.getGridField().toggle(false)

            this.form.ClosingDate.getGridField().toggle(false)
            this.form.ClosingType.getGridField().toggle(false)
            this.form.LostReason.getGridField().toggle(false)

            this.form.ContactPersonId.getGridField().toggle(false);
            this.form.ContactPersonPhone.getGridField().toggle(false);
          //  this.form.ProjectId.getGridField().toggle(false);
            this.form.ContactPersonProject.getGridField().toggle(false);
            this.form.ContactPersonAddress.getGridField().toggle(false);

            this.form.ContactsId.changeSelect2(e => {
                this.form.Products.ContactsId = Q.toId(this.form.ContactsId.value);
                this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Phone;
                this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Address;
                var isOrganisation = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization;

                this.form.ContactPersonId.getGridField().toggle(isOrganisation);
                this.form.ContactPersonPhone.getGridField().toggle(isOrganisation);

                if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts == true) {
                    this.form.ContactPersonProject.getGridField().toggle(isOrganisation);
                    this.form.ProjectId.getGridField().toggle(isOrganisation);
                }

                if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions == true) {
                    this.form.ContactsAddress.getGridField().toggle(!isOrganisation);
                    this.form.ContactPersonAddress.getGridField().toggle(isOrganisation);
                }

            });
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ToInvoice == true) {
                this.toolbar.findButton('ToInvoice').toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ToPerforma == true) {
                this.toolbar.findButton('ToPerforma').toggle(false);
            }


            if (this.form.PerDiscount.change || this.form.DiscountAmt.change) {
                if (this.form.PerDiscount.value > 0) {
                    var amt = this.form.Total.value * (this.form.PerDiscount.value / 100);
                    this.form.DiscountAmt.value = amt;
                    this.form.DisGrandTotal.value = this.form.GrandTotal.value - amt;
                }

                else if (this.form.DiscountAmt.value > 0) {
                    var dis = (this.form.DiscountAmt.value / this.form.Total.value) * 100;
                    this.form.PerDiscount.value = dis;
                    this.form.DisGrandTotal.value = this.form.GrandTotal.value - dis;
                }
                else {
                    this.form.DisGrandTotal.value = this.form.GrandTotal.value;
                }
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
                        //this.form.ProjectId.getGridField().toggle(isOrganisation);
                    }
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

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "2") {
                    Q.information("Setting status as Closed will also close all followups", () => { Q.resolveUrl('#'); });
                    this.form.ClosingDate.getGridField().toggle(true)
                  /*  this.form.ClosingDate.value = this.options.theDate;*/
                    this.form.ClosingType.getGridField().toggle(true);
                    this.form.ClosingDate.value = 'now';
                }
                else {
                    this.form.ClosingDate.getGridField().toggle(false)
                    this.form.ClosingType.getGridField().toggle(false)
                   // this.form.ClosingDate.value = "";
                    this.form.LostReason.getGridField().toggle(false)
                    this.form.ClosingDate.value = null;
                }
            });

            this.form.ClosingType.changeSelect2(e => {
                if (this.form.ClosingType.value == "2") {
                    this.form.LostReason.getGridField().toggle(true)
                }
                else {
                    this.form.LostReason.getGridField().toggle(false)
                }
            });

            this.form.SourceId.changeSelect2(e => {//Reference
                var SourceName = Masters.SourceRow.getLookup().itemById[this.form.SourceId.value].Source;
                if (SourceName == "Reference") {
                    this.form.ReferenceName.getGridField().toggle(true)
                    this.form.ReferencePhone.getGridField().toggle(true)
                }
                else {
                    this.form.ReferenceName.getGridField().toggle(false)
                    this.form.ReferencePhone.getGridField().toggle(false)
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

        }

        protected updateInterface() {

            // by default cloneButton is hidden in base UpdateInterface method
            super.updateInterface();

            //// here we show it if it is edit mode (not new)
            //if (Authorization.hasPermission("Quotation:Clone")) {
            //    this.cloneButton.toggle(this.isEditMode());
            //}
            this.toolbar.findButton('edit-roles-button').hide();

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        //Custom button inside dialog for Quotation Terms Master
        protected afterLoadEntity() {
            super.afterLoadEntity();
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("Quotation:Move To Quotation"))
            {
                buttons.push({
                    title: 'Clone',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Clone Quotation',
                    onClick: () => {
                        QuotationService.MoveToQuotation({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Quotation.QuotationDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
            }

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Quotation.PrintQuotation', //Change PrintQuotationUnique, PrintQuotation 
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

           
            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'BizWhatsApp',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                reportKey: 'Quotation.PrintQuotation',
                getParams: () => ({ Id: this.get_entityId(), ModType: "WAQuotation" }), //ModType Invoice, Invoice
                download: true //Set true if have to send mail
            }));
            //buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
            //    title: 'Whatsapp',
            //    icon: 'fa-whatsapp text-green',
            //    reportKey: 'Quotation.PrintQuotation', //Change PrintQuotationUnique, PrintQuotation
            //    getParams: () => ({ Id: this.get_entityId(), ModType: "WAQuotation" }), //ModType Quotation, Invoice
            //    download: true //Set true if have to send mail
            //}));

            buttons.push({

                title: 'SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send thankyou SMS to customer',
                onClick: () => {
                    QuotationService.SendSMS({
                        Id: Q.toId(this.form.ContactsId.value)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    
                },
                separator: true
            });

            buttons.push({
                title: 'BizWA',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                hint: 'Send thankyou Whatsapp to customer',
                onClick: () => {
                    QuotationService.SendWati({
                        Id: Q.toId(this.form.ContactsId.value)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });
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
            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'Quotation.PrintQuotation', //Change PrintQuotationUnique, PrintQuotation
                getParams: () => ({ Id: this.get_entityId(), ModType: "Quotation" }), //ModType Quotation, Invoice
                download: true, //Set true if have to send mail
                separator: true
            }
            ));
            


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

                        var template = Q.tryFirst(Template.QuotationTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
                }
            });

            if (Authorization.hasPermission("Quotation:Move to Invoice")) {
                buttons.push({
                    title: 'To Invoice',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move to Sales Invoice',
                    onClick: () => {
                        QuotationService.MoveToInvoice({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "Sales"
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                if (response.Id > 0)
                                    new Sales.SalesDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
            }

            if (Authorization.hasPermission("Quotation:Move to Proforma")) {
                buttons.push({
                    title: 'To Proforma',
                    icon: 'fa fa-share-square text-blue',
                    hint: 'Move to Proforma Invoice',
                    onClick: () => {
                        QuotationService.MoveToInvoice({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "Invoice"
                        },
                            response => {
                                if (response.Id > 0) {
                                    Q.notifyInfo(response.Status);
                                    new Sales.InvoiceDialog().loadByIdAndOpenDialog(response.Id);
                                }
                                else
                                    Q.notifyError(response.Status)
                            }
                        );
                    }
                });
            }
           // if (Authorization.hasPermission("Quotation:Move to Purchase")) {
                buttons.push({
                    title: 'To Purchase',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move to Purchase',
                    onClick: () => {
                        QuotationService.MoveToPurchase({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "Purchase"
                        },
                            response => {
                                console.log("Purchase Response:", response);
                                Q.notifyInfo(response.Status);
                                if (response.Id > 0)
                                    new Purchase.PurchaseDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
         //   }

            //if (Authorization.hasPermission("Quotation:Move to PurchaseOrder")) {
                buttons.push({
                    title: 'To PurchaseOrder',
                    icon: 'fa fa-share-square text-blue',
                    hint: 'Move to Purchase Order',
                    onClick: () => {
                        QuotationService.MoveToPurchase({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "PurchaseOrder"

                        },
                            response => {
                                if (response.Id > 0) {
                                    Q.notifyInfo(response.Status);
                                    new Purchase.PurchaseOrderDialog().loadByIdAndOpenDialog(response.Id);
                                }


                                else
                                    Q.notifyError(response.Status)
                            }
                        );
                    }
                });
          //  }



            if (Authorization.hasPermission("Quotation:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'approve-button',
                    icon: 'fa-check-circle text-green',
                    hint: 'Approve this Quotation',
                    onClick: () => {
                        QuotationService.Approve({
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

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].AppointmentInQuotation != true) {
                Serenity.TabsExtensions.toggle(this.tabs, 'Appointments', false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MultiCurrency != true) {
                this.form.CurrencyConversion.getGridField().toggle(false);
                this.form.Conversion.getGridField().toggle(false);
                this.form.FromCurrency.getGridField().toggle(false);
                this.form.ToCurrency.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MultiAddInfo != true) {
                this.form.QuotationAddinfoList.getGridField().toggle(true);
            }
            else {
                this.form.QuotationAddinfoList.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Addinfo2 != true) {
                this.form.AdditionalInfo2.getGridField().toggle(true);
            }
            else {
                this.form.AdditionalInfo2.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAdditionalCharges != true) {
                this.form.ChargesList.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DealerInQuotation!= true) {
                this.form.DealerId.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].QuoEditNo == true) {
                Serenity.EditorUtils.setReadonly(this.form.QuotationN.element, false);
            }


            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAdditionalConcessions != true) {
                this.form.ConcessionList.getGridField().toggle(false);
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

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                this.form.ContactPersonProject.getGridField().toggle(false);
            }

            if (this.form.CompanyId.value < "1") {
                this.form.CompanyId.value = Q.toId(Authorization.userDefinition.CompanyId);
            }

            if (this.form.Status.value == "2") {
                this.form.ClosingDate.getGridField().toggle(true)
                this.form.ClosingType.getGridField().toggle(true)
            }

            if (this.form.ClosingType.value == "2") {
                this.form.LostReason.getGridField().toggle(true)
            }

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.ContactsId.value >= "1") {
                if (Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization) {
                    this.form.ContactPersonId.getGridField().toggle(true);
                    this.form.ContactPersonPhone.getGridField().toggle(true);
                    this.form.ContactPersonProject.getGridField().toggle(true);
                    //this.form.ProjectId.getGridField().toggle(true);
                    this.form.ContactPersonAddress.getGridField().toggle(true);

                    this.form.ContactsAddress.getGridField().toggle(false);
                }
                else {
                    this.form.ContactPersonId.getGridField().toggle(false);
                    this.form.ContactPersonPhone.getGridField().toggle(false);
                    //this.form.ProjectId.getGridField().toggle(false);
                    this.form.ContactPersonProject.getGridField().toggle(false);
                    this.form.ContactPersonAddress.getGridField().toggle(false);

                    this.form.ContactsAddress.getGridField().toggle(true);
                }
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions != true) {
                this.form.ContactsAddress.getGridField().toggle(false);
                this.form.ContactPersonAddress.getGridField().toggle(false);
            }

            if (this.form.QuotationNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                QuotationService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                        this.form.QuotationN.value = Q.trimToNull(response.SerialN);
                    this.form.QuotationNo.value = Q.toId(response.Serial);
                });
            }


            if (Administration.CompanyDetailsRow.getLookup().itemById[1].WinPercentageInQuotation != true) {
                this.form.WinPercentage.getGridField().toggle(false); // Hide if false
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ExpectedClosingDateInQuotation != true) {
                this.form.ExpectedClosingDate.getGridField().toggle(false); // Hide if false
            }

            
        }

        loadEntity(entity: QuotationRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Appointments', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (this.form.PerDiscount.value > 0) {
                var amt = this.form.Total.value * (this.form.PerDiscount.value / 100);
                this.form.DiscountAmt.value = amt;
                this.form.DisGrandTotal.value = this.form.GrandTotal.value - amt;
            }
            else if (this.form.DiscountAmt.value > 0) {
                var dis = (this.form.DiscountAmt.value / this.form.Total.value) * 100;
                this.form.PerDiscount.value = dis;
                this.form.DisGrandTotal.value = this.form.GrandTotal.value - dis;
            }

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.QuotationId = entity.Id.toString();
                this.appointmentsGrid.QuotationId = entity.Id.toString();
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