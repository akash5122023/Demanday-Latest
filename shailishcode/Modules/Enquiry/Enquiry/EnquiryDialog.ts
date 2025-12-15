namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class EnquiryDialog extends DialogBase<EnquiryRow, any> {
        protected getFormKey() { return EnquiryForm.formKey; }
        protected getIdProperty() { return EnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryRow.nameProperty; }
        protected getService() { return EnquiryService.baseUrl; }
        protected getDeletePermission() { return EnquiryRow.deletePermission; }
        protected getInsertPermission() { return EnquiryRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryRow.updatePermission; }

        protected form = new EnquiryForm(this.idPrefix);

        private followupsGrid: EnquiryFollowupsGrid;
        private appointmentsGrid: EnquiryAppointmentsGrid;

        constructor() {
            super();

            this.followupsGrid = new EnquiryFollowupsGrid(this.byId('FollowupsGrid'));
            this.appointmentsGrid = new EnquiryAppointmentsGrid(this.byId('AppointmentsGrid'));

            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            this.form.ReferenceName.getGridField().toggle(false)
            this.form.ReferencePhone.getGridField().toggle(false)



            this.form.ClosingDate.getGridField().toggle(false)
            this.form.ClosingType.getGridField().toggle(false)
            this.form.LostReason.getGridField().toggle(false)

            this.form.ContactPersonId.getGridField().toggle(false);
            this.form.ContactPersonPhone.getGridField().toggle(false);
            this.form.ContactPersonProject.getGridField().toggle(false);
          //  this.form.ProjectId.getGridField().toggle(false);
            this.form.ContactPersonAddress.getGridField().toggle(false);

            Q.reloadLookup(Contacts.ContactsRow.lookupKey);
            this.form.ContactsId.changeSelect2(e => {
                var conid = Q.toId(this.form.ContactsId.value);
                if (conid != null) {
                    this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[conid].Phone;
                    this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[conid].Address;
                    var isOrganisation = Contacts.ContactsRow.getLookup().itemById[conid].ContactType == Masters.CTypeMaster.Organization;

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

          //if(this.form.OwnerId.value != null)
          //  {
          //      var owner = Q.toId(this.form.OwnerId.value);
          //      if (owner != null) {
          //          this.form.CompanyId.value = Q.toId(Administration.UserRow.getLookup().itemById[owner].CompanyId);
          //      }
          //  }

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == "2") {
                    Q.information("Setting status as Closed will also close all followups", () => { Q.resolveUrl('#'); });
                    this.form.ClosingDate.getGridField().toggle(true)
                    //this.form.ClosingDate.get_value() = Date.now
                    this.form.ClosingType.getGridField().toggle(true)
                }
                else {
                    this.form.ClosingDate.getGridField().toggle(false)
                    this.form.ClosingType.getGridField().toggle(false)
                    this.form.LostReason.getGridField().toggle(false)
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

           
        }

        protected updateInterface() {
            super.updateInterface();

           //  here we show it if it is edit mode (not new)
            if (Authorization.hasPermission("Enquiry:Clone")) {
                this.cloneButton.toggle(this.isEditMode());
            }

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("Enquiry:Move to Enquiry")) {
                buttons.push({
                    title: 'Clone',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Clone',
                    onClick: () => {
                        EnquiryService.MoveToEnquiry({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Enquiry.EnquiryDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
            }

            buttons.push({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send mail of thankyou to customer',
                onClick: () => {
                    EnquiryService.SendMail({
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
                hint: 'Send thankyou SMS to customer',
                onClick: () => {
                    EnquiryService.SendSMS({
                        Id: Q.toId(this.form.ContactsId.value)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            //buttons.push({
            //    title: 'BizWA',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Send thankyou Whatsapp to customer',
            //    onClick: () => {
            //        EnquiryService.SendWati({
            //            Id: Q.toId(this.form.ContactsId.value)
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

                        // Create an array with a single object
                        const contacts = [{ number: num, name: name }];

                        var dialog = new Common.IntractWaDialog({ Contacts: contacts });

                        //var dialog = new Common.IntractWaDialog({
                        //    Number: num,
                        //    Name: name,  
                        //});

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

                        var template = Q.tryFirst(Template.EnquiryTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
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

            

            if (Authorization.hasPermission("Enquiry:Move to Quotation")) {
                buttons.push({
                    title: 'To Quotation',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Move to Quotation',
                    onClick: () => {
                        EnquiryService.MoveToQuotation({
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

            return buttons;
        }


        onDialogOpen() {
            super.onDialogOpen();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RequirementInEnquiry != true) {
                this.form.Products.element.closest('.category').toggle(false);
                this.form.Products.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MultiAddInfo != true) {
                this.form.EnquiryAddinfoList.getGridField().toggle(true);
            }
            else {
                this.form.EnquiryAddinfoList.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Addinfo2 != true) {
                this.form.AdditionalInfo2.getGridField().toggle(true);
            }
            else
            {
                this.form.AdditionalInfo2.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].AppointmentInEnquiry != true) {
                Serenity.TabsExtensions.toggle(this.tabs, 'Appointments', false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DealerInEnquiry != true) {
                this.form.DealerId.getGridField().toggle(false);
            }
            else
            {
                this.form.DealerId.getGridField().toggle(true);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnqEditNo == true) {
                Serenity.EditorUtils.setReadonly(this.form.EnquiryN.element, false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                this.form.ContactPersonProject.getGridField().toggle(false);
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
                   // this.form.ProjectId.getGridField().toggle(true);
                    this.form.ContactPersonAddress.getGridField().toggle(true);

                    this.form.ContactsAddress.getGridField().toggle(false);
                }
                else {
                    this.form.ContactPersonId.getGridField().toggle(false);
                    this.form.ContactPersonPhone.getGridField().toggle(false);
                    this.form.ContactPersonProject.getGridField().toggle(false);
                    this.form.ContactPersonAddress.getGridField().toggle(false);
                  //  this.form.ProjectId.getGridField().toggle(false);
                    this.form.ContactsAddress.getGridField().toggle(true);
                }
            }

            if (this.form.EnquiryNo.value < 1) {

                EnquiryService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.EnquiryN.value = Q.trimToNull(response.SerialN);
                    this.form.EnquiryNo.value = Q.toId(response.Serial);
                });
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions != true) {
                this.form.ContactsAddress.getGridField().toggle(false);
                this.form.ContactPersonAddress.getGridField().toggle(false);
            }



            if (Administration.CompanyDetailsRow.getLookup().itemById[1].WinPercentageInEnquiry != true) {
                this.form.WinPercentage.getGridField().toggle(false); // Hide if false
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ExpectedClosingDateInEnquiry != true) {
                this.form.ExpectedClosingDate.getGridField().toggle(false); // Hide if false
            }



        }

        loadEntity(entity: EnquiryRow) {
            super.loadEntity(entity);
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Appointments', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.enquiryId = entity.Id.toString();
                this.appointmentsGrid.enquiryId = entity.Id.toString();
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