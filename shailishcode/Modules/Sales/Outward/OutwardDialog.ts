
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class OutwardDialog extends DialogBase<OutwardRow, any> {
        protected getFormKey() { return OutwardForm.formKey; }
        protected getIdProperty() { return OutwardRow.idProperty; }
        protected getLocalTextPrefix() { return OutwardRow.localTextPrefix; }
        protected getNameProperty() { return OutwardRow.nameProperty; }
        protected getService() { return OutwardService.baseUrl; }
        protected getDeletePermission() { return OutwardRow.deletePermission; }
        protected getInsertPermission() { return OutwardRow.insertPermission; }
        protected getUpdatePermission() { return OutwardRow.updatePermission; }


        protected form = new OutwardForm(this.idPrefix);
        constructor() {
            super();

            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));

            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");

                        if (Authorization.hasPermission("Outward:Change Branch")) {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, false);
                        }
                        else {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, true);
                        }
                    }
                }
            );

            this.form.ShippingAddress.getGridField().toggle(false);

            this.form.ContactPersonId.getGridField().toggle(false);
            this.form.ContactPersonPhone.getGridField().toggle(false);
            this.form.ContactPersonProject.getGridField().toggle(false);
            this.form.ContactPersonAddress.getGridField().toggle(false);

            this.form.OtherAddress.change(e => {
                if (this.form.OtherAddress.value == true) {
                    this.form.ShippingAddress.getGridField().toggle(true);
                }
                else {
                    this.form.ShippingAddress.getGridField().toggle(false);
                }
            });

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

            this.form.Status.changeSelect2(e => {
                if (this.form.Status.value == 2) {
                    this.form.ClosingDate.getGridField().toggle(true)
                }
                else {
                    this.form.ClosingDate.getGridField().toggle(false)
                }
            });
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
                reportKey: 'Outward.PrintOutward',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'Outward.PrintOutward',
                getParams: () => ({ Id: this.get_entityId(), ModType: "Outward" }), //ModType Quotation, Invoice
                download: true //Set true if have to send mail
            }));

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

                        var template = Q.tryFirst(Template.ChallanTemplateRow.getLookup().items, x => x.CompanyId == Authorization.userDefinition.CompanyId).SMSTemplate;
                        template = template.replace("#customername", name)
                        var str = "https://api.whatsapp.com/send?phone=" + phn + "&text=" + encodeURI(template);

                        window.open(str, '_blank')
                    }
                    else
                        Q.notifyError("Contact Invalid!!");
                }
            });

            if (Authorization.hasPermission("Outward:Move to Inward")) {
                buttons.push({
                    title: 'To Inward',
                    icon: 'fa fa-share-square text-green',
                    hint: 'Move to Inward',
                    onClick: () => {
                        OutwardService.MoveToInward({ //Disable if not applicable
                            Id: this.get_entityId(),
                            MailType: "Sales"
                        },
                            response => {
                                if (response.Id > 0) {
                                    Q.notifyInfo(response.Status);
                                    new InwardDialog().loadByIdAndOpenDialog(response.Id);
                                }
                                else
                                    Q.notifyError(response.Status)
                            }
                        );
                    }
                });
            }
            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.OwnerId.value < '1') {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < '1') {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.ContactsId.value >= '1') {
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

            if (this.form.Status.value != 2) {
                this.form.ClosingDate.getGridField().toggle(false);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions != true) {
                this.form.ContactsAddress.getGridField().toggle(false);
                this.form.ContactPersonAddress.getGridField().toggle(false);
            }

            if (this.form.BranchId.value < '1' && Authorization.userDefinition.BranchId) {
                this.form.BranchId.value = Authorization.userDefinition.BranchId.toString().toString();
            }

            if (this.form.OtherAddress.value == true) {
                this.form.ShippingAddress.getGridField().toggle(true);
            }
            else {
                this.form.ShippingAddress.getGridField().toggle(false);
            }

            if (this.form.ChallanNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                OutwardService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.ChallanNo.value = Q.toId(response.Serial);
                });
            }
        }
    }
}