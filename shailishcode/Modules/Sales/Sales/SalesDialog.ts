
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class SalesDialog extends DialogBase<SalesRow, any> {
        protected getFormKey() { return SalesForm.formKey; }
        protected getIdProperty() { return SalesRow.idProperty; }
        protected getLocalTextPrefix() { return SalesRow.localTextPrefix; }
        protected getNameProperty() { return SalesRow.nameProperty; }
        protected getService() { return SalesService.baseUrl; }
        protected getDeletePermission() { return SalesRow.deletePermission; }
        protected getInsertPermission() { return SalesRow.insertPermission; }
        protected getUpdatePermission() { return SalesRow.updatePermission; }

        protected form = new SalesForm(this.idPrefix);

        private followupsGrid: SalesFollowupsGrid;

        constructor() {
            super();

            this.followupsGrid = new SalesFollowupsGrid(this.byId('FollowupsGrid'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");

                        if (Authorization.hasPermission("Sales:Change Branch")) {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, false);
                        }
                        else {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, true);
                        }
                    }
                }
            );

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

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("Sales:Move to Clone")) {
                buttons.push({
                    title: 'Clone',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Clone Sales',
                    onClick: () => {
                        SalesService.MoveToClone({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Sales.SalesDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
            }

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Sales.PrintSale',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'WaInvoice',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                reportKey: 'Sales.PrintSale',
                getParams: () => ({ Id: this.get_entityId(), ModType: "WASales" }), //ModType Invoice, Invoice
                download: true //Set true if have to send mail
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'Sales.PrintSale',
                getParams: () => ({ Id: this.get_entityId(), ModType: "Sales" }), //ModType Quotation, Invoice, Sales
                download: true //Set true if have to send mail
            }));

            buttons.push({

                title: 'SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send thankyou SMS to customer',
                onClick: () => {
                    SalesService.SendSMS({
                        Id: Q.toId(this.get_entityId())
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }

            });

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

            //buttons.push({
            //    title: 'BizWA',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Send thankyou Whatsapp to customer',
            //    onClick: () => {
            //        SalesService.SendWati({
            //            Id: Q.toId(this.entityId)
            //        },
            //            response => {
            //                Q.notifyInfo(response.Status);
            //            }
            //        );
            //    }
            //});

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
            if (Authorization.hasPermission("Cashbook:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'approve-button',
                    icon: 'fa-check-circle text-green',
                    hint: 'Approve this',
                    onClick: () => {
                        SalesService.Approve({
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

            if (this.form.BranchId.value < "1" && Authorization.userDefinition.BranchId && Authorization.userDefinition.BranchId) {
                this.form.BranchId.value = Authorization.userDefinition.BranchId.toString();
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RoundupInSales == true) {
                this.form.Roundup.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].AdvanceInSales == true) {
                this.form.Advacne.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].StageInSales == true) {
                this.form.StageId.getGridField().toggle(false);
            }

            if (this.form.OwnerId.value < "1") {
                this.form.CompanyId.value = Q.toId(Authorization.userDefinition.CompanyId);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].PackagingInSales == true) {
                this.form.PackagingCharges.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].FreightInSales == true) {
                this.form.FreightCharges.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DueDateInSales == true) {
                this.form.DueDate.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DispatchInSales == true) {
                this.form.DispatchDetails.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].GstDetailsInSales == true) {
                //this.element.find($('a[name="AdvanceCRM_Default_SalesDialog16_Category3"]')).toggle(false);
                this.form.ReverseCharge.element.closest('.category').toggle(false);
                this.form.ReverseCharge.getGridField().toggle(false);
                this.form.EcomType.getGridField().toggle(false);
                this.form.InvoiceType.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TermsInSales == true) {
                this.form.TermsList.element.closest('.category').toggle(false);
                this.form.TermsList.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DealerInInvoice != true) {
                this.form.DealerId.getGridField().toggle(false);
            }
            else
            {
                this.form.DealerId.getGridField().toggle(true);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].InvEditNo == true) {
                Serenity.EditorUtils.setReadonly(this.form.InvoiceN.element, false);
            }


            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                this.form.ContactPersonProject.getGridField().toggle(false);
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

            if (this.form.Status.value != '2') {
                this.form.ClosingDate.getGridField().toggle(false);
            }

            if (this.form.OtherAddress.value == true) {
                this.form.ShippingAddress.getGridField().toggle(true);
            }
            else {
                this.form.ShippingAddress.getGridField().toggle(false);
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

            if (this.form.InvoiceNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                SalesService.GetNextNumber({
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
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.SalesId = entity.Id.toString();
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