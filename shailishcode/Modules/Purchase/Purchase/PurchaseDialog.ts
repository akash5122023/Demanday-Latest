
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class PurchaseDialog extends DialogBase<PurchaseRow, any> {
        protected getFormKey() { return PurchaseForm.formKey; }
        protected getIdProperty() { return PurchaseRow.idProperty; }
        protected getLocalTextPrefix() { return PurchaseRow.localTextPrefix; }
        protected getNameProperty() { return PurchaseRow.nameProperty; }
        protected getService() { return PurchaseService.baseUrl; }
        protected getDeletePermission() { return PurchaseRow.deletePermission; }
        protected getInsertPermission() { return PurchaseRow.insertPermission; }
        protected getUpdatePermission() { return PurchaseRow.updatePermission; }

        protected form = new PurchaseForm(this.idPrefix);

        constructor() {
            super();
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));


            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");

                        if (Authorization.hasPermission("Purchase:Change Branch")) {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, false);
                        }
                        else {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, true);
                        }
                    }
                }
            );

            this.form.PurchaseFromPhone.change(e => {
                var code = Q.text(this.form.PurchaseFromPhone.value);
                if (code.trim() != "") {
                    this.form.PurchaseFromId.value = Q.tryFirst(Contacts.ContactsRow.getLookup().items, x => x.Phone == code).Id.toString();
                    //this.form.ContactsAddress.value = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].Address;
                    //var isOrganisation = Contacts.ContactsRow.getLookup().itemById[this.form.ContactsId.value].ContactType == Masters.CTypeMaster.Organization;

                    //this.form.ContactPersonId.getGridField().toggle(isOrganisation);
                    //this.form.ContactPersonPhone.getGridField().toggle(isOrganisation);

                    //if (Administration.CompanyDetailsRow.getLookup().itemById[1].EnableAddressInTransactions == true) {
                    //    this.form.ContactsAddress.getGridField().toggle(!isOrganisation);
                    //    this.form.ContactPersonAddress.getGridField().toggle(isOrganisation);
                    //}

                    //if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts == true) {
                    //    this.form.ContactPersonProject.getGridField().toggle(isOrganisation);
                    //}
                }
            });

            this.form.PurchaseFromId.changeSelect2(e => {
                //this.form.Products.ContactsId = Q.toId(this.form.ContactsId.value);
                this.form.PurchaseFromPhone.value = Contacts.ContactsRow.getLookup().itemById[this.form.PurchaseFromId.value].Phone;
            });

            //if (Default.CompanyDetailsRow.getLookup().itemById[1].StockType == 2) {
            //    this.form.BranchId.element.addClass(" required");
            //}

            //if (CompanyDetailsRow.getLookup().itemById[1].StockType == 2) {
            //    if (Authorization.hasPermission("Purchase:Purchase:Change Branch")) {
            //        this.form.BranchId.readOnly = false;
            //    }
            //    else {
            //        this.form.BranchId.readOnly = true;
            //    }
            //}
        }



        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("Purchase:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'approve-button',
                    icon: 'fa-check-circle text-green',
                    hint: 'Approve this Expense',
                    onClick: () => {
                        PurchaseService.Approve({
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
            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.OwnerId.value < '1') {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < '1') {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.BranchId.value < "1" && Authorization.userDefinition.BranchId) {
                this.form.BranchId.value = Authorization.userDefinition.BranchId.toString().toString();
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RoundupInPurchase != true) {
                this.form.Roundup.getGridField().toggle(false);
            }

            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
        }
    }
}