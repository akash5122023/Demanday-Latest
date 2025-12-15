
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class CMSDialog extends DialogBase<CMSRow, any> {
        protected getFormKey() { return CMSForm.formKey; }
        protected getIdProperty() { return CMSRow.idProperty; }
        protected getLocalTextPrefix() { return CMSRow.localTextPrefix; }
        protected getNameProperty() { return CMSRow.nameProperty; }
        protected getService() { return CMSService.baseUrl; }
        protected getDeletePermission() { return CMSRow.deletePermission; }
        protected getInsertPermission() { return CMSRow.insertPermission; }
        protected getUpdatePermission() { return CMSRow.updatePermission; }

        protected form = new CMSForm(this.idPrefix);

        private followupsGrid: CMSFollowupsGrid;
        constructor() {
            super();

            this.followupsGrid = new CMSFollowupsGrid(this.byId('FollowupsGrid'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));


          /*  this.form.SerialNo.element.addClass(" required");*/

            this.form.Amount.getGridField().toggle(false)

            this.form.Category.changeSelect2(e => {
                if (this.form.Category.value == "1") {
                    this.form.Amount.getGridField().toggle(true)
                }
                else {
                    this.form.Amount.getGridField().toggle(false)
                }
            });

            this.form.InvestigationBy.getGridField().toggle(false);
            this.form.Observation.getGridField().toggle(false);
            this.form.ActionBy.getGridField().toggle(false);
            this.form.Action.getGridField().toggle(false);
            this.form.SupervisedBy.getGridField().toggle(false);
            this.form.Comments.getGridField().toggle(false);
            this.form.CompletionDate.getGridField().toggle(false);
            this.form.Feedback.getGridField().toggle(false);

            this.form.Status.changeSelect2(e => {

                switch (this.form.Status.value) {
                    case "2":
                        this.form.CompletionDate.getGridField().toggle(true);
                        this.form.Feedback.getGridField().toggle(true);
                        this.form.CompletionDate.valueAsDate = new Date();

                    case "5":
                        this.form.SupervisedBy.getGridField().toggle(true);
                        this.form.Comments.getGridField().toggle(true);

                    case "4":
                        this.form.ActionBy.getGridField().toggle(true);
                        this.form.Action.getGridField().toggle(true);

                    case "3":
                        this.form.InvestigationBy.getGridField().toggle(true);
                        this.form.Observation.getGridField().toggle(true);
                }
            });

            this.form.ContactsId.changeSelect2(e => {
                var CId = Q.toId(this.form.ContactsId.value);

                if (CId > 0) {
                    this.form.ContactsPhone.value = Contacts.ContactsRow.getLookup().itemById[CId].Phone;
                    this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[CId].Address;
                    var prod = Q.tryFirst(Sales.SalesProductsRow.getLookup().items, x => x.SalesContactsId == CId);
                    this.form.ProductsId.value = prod ? prod.ProductsId.toString() : "";
                    this.form.SerialNo.value = prod ? prod.Serial.toString() : "";
                }
                else {
                    this.form.ContactsPhone.value = "";
                    this.form.ContactsAddress.value = "";
                }

            });

            this.form.DealerId.changeSelect2(e => {
                var DId = Q.toId(this.form.DealerId.value);

                if (DId > 0) {
                    this.form.DealerPhone.value = Masters.DealerRow.getLookup().itemById[DId].Phone;
                    this.form.DealerEmail.value = Masters.DealerRow.getLookup().itemById[DId].Email;
                   }
                else {
                    this.form.DealerPhone.value = "";
                    this.form.DealerEmail.value = "";
                }

            });

            this.form.EmployeeId.changeSelect2(e => {
                var EId = Q.toId(this.form.EmployeeId.value);

                if (EId > 0) {
                    this.form.EmployeeEmail.value = Employee.EmployeeRow.getLookup().itemById[EId].Email;
                    this.form.EmployeePhone.value = Employee.EmployeeRow.getLookup().itemById[EId].Phone;
                    }
                else {
                    this.form.EmployeeEmail.value = "";
                    this.form.EmployeePhone.value = "";
                }

            });

            this.form.ContactsPhone.change(e => {

                var phone = Q.text(this.form.ContactsPhone.value);
                this.form.ContactsId.value = Q.tryFirst(Contacts.ContactsRow.getLookup().items, x => x.Phone == phone).Id.toString();
                var CId = Q.toId(this.form.ContactsId.value);
                this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[CId].Address;
                var prod = Q.tryFirst(Sales.SalesProductsRow.getLookup().items, x => x.SalesContactsId == CId);
                this.form.ProductsId.value = prod ? prod.ProductsId.toString() : "";
                this.form.SerialNo.value = prod ? prod.Serial.toString() : "";
            });


            this.form.SerialNo.change(e => {

                var serial = Q.text(this.form.SerialNo.value);

                //Q.notifyInfo(serial);
                var prod = Q.tryFirst(Sales.SalesProductsRow.getLookup().items, x => x.Serial == serial);
                this.form.ProductsId.value = prod ? prod.ProductsId.toString() : "";
                this.form.ContactsId.value = prod ? prod.SalesContactsId.toString() : "";
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

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'CMS.PrintCMS',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));
            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'BizWhatsApp',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                reportKey: 'CMS.PrintCMS',
                getParams: () => ({ Id: this.get_entityId(), ModType: "WACMS" }), //ModType Invoice, Invoice
                download: true //Set true if have to send mail
            }));
            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Send Receipt',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'CMS.PrintCMS',
                getParams: () => ({ Id: this.get_entityId(), ModType: "CMS" }), //ModType Quotation, CMS, CMS
                download: true //Set true if have to send mail
            }));


            buttons.push({

                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send CMS details over mail',
                onClick: () => {
                    CMSService.SendMail({
                        Id: this.get_entityId(),
                        MailType: "Customer"
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
                hint: 'Send CMS SMS to customer',
                onClick: () => {
                    CMSService.SendSMS({
                        Id: Q.toId(this.get_entityId()),
                        SMSType: "Customer",
                        EngineerID: Q.toId(this.form.AssignedTo.value)
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

                        var template = Q.tryFirst(Template.CmsTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
                }
            });

            buttons.push({

                title: 'Eng. Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send CMS mail to engineer',
                onClick: () => {
                    CMSService.SendMail({
                        Id: this.get_entityId(),
                        MailType: "Engineer"
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            buttons.push({

                title: 'Eng. SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send CMS SMS to engineer',
                onClick: () => {
                    CMSService.SendSMS({
                        Id: Q.toId(this.get_entityId()),
                        SMSType: "Engineer",
                        EngineerID: Q.toId(this.form.AssignedTo.value)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });

            buttons.push({

                title: 'Closed Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send CMS closed mail to customer',
                onClick: () => {
                    if (this.form.CompletionDate.value != null) {
                        CMSService.SendMail({
                            Id: this.get_entityId(),
                            MailType: "ClosedMail"
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );
                    }
                    else {
                        Q.alert("Cannot send mail before closing complaint");
                    }
                }
            });

            buttons.push({

                title: 'Closed SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send CMS closed SMS to customer',
                onClick: () => {
                    if (this.form.CompletionDate.value != null) {
                        CMSService.SendSMS({
                            Id: Q.toId(this.get_entityId()),
                            SMSType: "ClosedSMS",
                            EngineerID: Q.toId(this.form.AssignedTo.value)
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );
                    }
                    else {
                        Q.alert("Cannot send SMS before closing complaint");
                    }
                }
            });

            if (Authorization.hasPermission("Enquiry:Move to Quotation")) {
                buttons.push({
                    title: 'To Quotation',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Generate Quotation for added products',
                    onClick: () => {
                        CMSService.MoveToQuotation({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Quotation.QuotationDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    },
                    separator: true
                });
            }

            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectInCms != true) {
                this.form.Products.element.closest('.category').toggle(false);
                this.form.Products.getGridField().toggle(false);
            }
            //var ser = Administration.CompanyDetailsRow.getLookup().itemById[1].ServicePerson;
            //if (ser != true) {
            //    this.form.EmployeeId.element.closest('.category').toggle(false);
            //    this.form.EmployeeId.getGridField().toggle(false);
            //    this.form.EmployeePhone.getGridField().toggle(false);
            //    this.form.EmployeeEmail.getGridField().toggle(false);
            //}
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].CmsEditNo == true) {
                Serenity.EditorUtils.setReadonly(this.form.Cmsn.element, false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RemovePurchaseDate != true) {
                 this.form.PurchaseDate.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RemoveInvoiceNo != true) {
                this.form.InvoiceNo.getGridField().toggle(false);
            }

            if (this.form.AssignedBy.value < "1") {
                this.form.AssignedBy.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedTo.value < "1") {
                this.form.AssignedTo.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].DealerInCms != 1) {
                this.form.DealerId.element.closest('.category').toggle(false);
                this.form.DealerId.getGridField().toggle(false);
                this.form.DealerEmail.getGridField().toggle(false);
                this.form.DealerPhone.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ServicePerson != 1) {
                 this.form.EmployeeId.element.closest('.category').toggle(false);
                this.form.EmployeeId.getGridField().toggle(false);
                this.form.EmployeePhone.getGridField().toggle(false);
                this.form.EmployeeEmail.getGridField().toggle(false);
            }
            

            Serenity.EditorUtils.setReadonly(this.form.AssignedBy.element, true);

            switch (this.form.Status.value) {
                case "2":
                    this.form.CompletionDate.getGridField().toggle(true);
                    this.form.Feedback.getGridField().toggle(true);
                    this.form.CompletionDate.valueAsDate = new Date();

                case "5":
                    this.form.SupervisedBy.getGridField().toggle(true);
                    this.form.Comments.getGridField().toggle(true);

                case "4":
                    this.form.ActionBy.getGridField().toggle(true);
                    this.form.Action.getGridField().toggle(true);

                case "3":
                    this.form.InvestigationBy.getGridField().toggle(true);
                    this.form.Observation.getGridField().toggle(true);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].CmsEditNo == true) {
                if (this.form.CMSNo.value < 1) {
                    // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                    CMSService.GetNextNumber({
                        Prefix: '',
                        Length: 5,  //we want service to search for and return serials of 5 in length
                    }, response => {
                        this.form.Cmsn.value = Q.trimToNull(response.SerialN);
                        this.form.CMSNo.value = Q.toId(response.Serial);
                    });
                }
            }
        }

        loadEntity(entity: CMSRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Followups', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.followupsGrid.CMSId = entity.Id.toString();
            }
        }
    }
}