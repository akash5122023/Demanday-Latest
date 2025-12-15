
namespace AdvanceCRM.Contacts {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class ContactsDialog extends DialogBase<ContactsRow, any> {
        protected getFormKey() { return ContactsForm.formKey; }
        protected getIdProperty() { return ContactsRow.idProperty; }
        protected getLocalTextPrefix() { return ContactsRow.localTextPrefix; }
        protected getNameProperty() { return ContactsRow.nameProperty; }
        protected getService() { return ContactsService.baseUrl; }
        protected getDeletePermission() { return ContactsRow.deletePermission; }
        protected getInsertPermission() { return ContactsRow.insertPermission; }
        protected getUpdatePermission() { return ContactsRow.updatePermission; }

        protected form = new ContactsForm(this.idPrefix);
        private channel: boolean;

        constructor() {
            super();

            this.element.find(".category-title:contains('Bank Details')").parent().toggle(false);
            this.channel = true;


            Common.NavigationService.ChannelsManagement({},
                response => {
                    if (response.Status == "Remove") {
                        this.channel = false;
                        this.element.find(".category-title:contains('Channels')").parent().toggle(false);
                    }
                }
            );

            //Category of customer
            this.form.CustomerType.changeSelect2(e => {
                if (this.form.CustomerType.value == "1") {
                    this.form.CategoryId.filterField = "Type";
                    this.form.CategoryId.filterValue = 1;
                }
                else if (this.form.CustomerType.value == "2") {
                    this.form.CategoryId.filterField = "Type";
                    this.form.CategoryId.filterValue = 2;

                }
            });

            //Vendor or Customer
            this.form.CustomerType.change(e => {
                if (this.form.CustomerType.value == "2") {
                    this.element.find(".category-title:contains('Bank Details')").parent().toggle(true);
                    this.element.find(".category-title:contains('Channels')").parent().toggle(true);
                }
                else {
                    this.element.find(".category-title:contains('Bank Details')").parent().toggle(false);
                    if (this.channel) {
                        this.element.find(".category-title:contains('Channels')").parent().toggle(true);
                    }
                }
            });

            this.form.Name.change(e => {

                this.form.Name.value = this.form.Name.value.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1); });

            });

            this.form.ContactType.change(e => {
                if (this.form.ContactType.value == "1") {
                    this.form.AccountsEmail.getGridField().toggle(false);
                    this.form.PurchaseEmail.getGridField().toggle(false);
                    this.form.SalesEmail.getGridField().toggle(false);
                    this.form.ServiceEmail.getGridField().toggle(false);
                    this.form.SubContacts.getGridField().toggle(false);
                    this.form.ResidentialPhone.getGridField().toggle(true);
                    this.form.OfficePhone.getGridField().toggle(true);
                    this.form.Gender.getGridField().toggle(true);
                    this.form.Religion.getGridField().toggle(true);
                    this.form.AreaId.getGridField().toggle(true);
                    this.form.MaritalStatus.getGridField().toggle(true);
                    this.form.MarriageAnniversary.getGridField().toggle(false);
                    this.form.Birthdate.getGridField().toggle(true);
                    this.form.DateOfIncorporation.getGridField().toggle(false);
                }
                else if (this.form.ContactType.value == "2") {
                    this.form.AccountsEmail.getGridField().toggle(true);
                    this.form.PurchaseEmail.getGridField().toggle(true);
                    this.form.SalesEmail.getGridField().toggle(true);
                    this.form.ServiceEmail.getGridField().toggle(true);
                    this.form.SubContacts.getGridField().toggle(true);
                    this.form.ResidentialPhone.getGridField().toggle(false);
                    this.form.OfficePhone.getGridField().toggle(false);
                    this.form.Gender.getGridField().toggle(false);
                    this.form.Religion.getGridField().toggle(false);
                    this.form.AreaId.getGridField().toggle(true);
                    this.form.MaritalStatus.getGridField().toggle(false);
                    this.form.MarriageAnniversary.getGridField().toggle(false);
                    this.form.Birthdate.getGridField().toggle(false);
                    this.form.DateOfIncorporation.getGridField().toggle(true);
                }
            });
            this.form.Phone.change(e => {
                if (this.form.Phone.value == "") {
                    this.form.Whatsapp.value = "+91 " + this.form.Phone.value;
                }
            });

            this.form.MaritalStatus.change(e => {
                if (this.form.MaritalStatus.value == "2") {
                    this.form.MarriageAnniversary.getGridField().toggle(true);
                }
                else {
                    this.form.MarriageAnniversary.getGridField().toggle(false);
                }
            });

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].PassportDetails != true) {

                this.form.PassportNumber.getGridField().toggle(false);
                this.form.FirstName.getGridField().toggle(false);
                this.form.LastName.getGridField().toggle(false);
                this.form.ExpiryDate.getGridField().toggle(false);
                this.element.find(".category-title:contains('PassportDetails')").parent().toggle(false);

            }
            else {

                this.form.PassportNumber.getGridField().toggle(true);
                this.form.FirstName.getGridField().toggle(true);
                this.form.LastName.getGridField().toggle(true);
                this.form.ExpiryDate.getGridField().toggle(true);
                this.element.find(".category-title:contains('PassportDetails')").parent().toggle(true);


            }

            //this.form.Country.addValidationRule(this.uniqueName, e => {
            //    if (!(/someregex/.test(this.form.Country.value)) return "It doesn't match the regex!";
            //});


          //  Regex regex = new Regex(@"^\d{6}$"); 
            this.form.Country.changeSelect2(e => {
                if (this.form.Country.value == "81") {
                   
                     }
            })

            //Hiding channel fields
            this.form.NationalDistributor.getGridField().toggle(false);
            this.form.Stockist.getGridField().toggle(false);
            this.form.Distributor.getGridField().toggle(false);
            this.form.Dealer.getGridField().toggle(false);
            this.form.Wholesaler.getGridField().toggle(false);
            this.form.Reseller.getGridField().toggle(false);

            this.form.ChannelCategory.change(e => {
                if (this.form.ChannelCategory.value == null) { //Nothing
                    this.form.NationalDistributor.getGridField().toggle(false);
                    this.form.Stockist.getGridField().toggle(false);
                    this.form.Distributor.getGridField().toggle(false);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "1") { //Direct Customer
                    this.form.NationalDistributor.getGridField().toggle(false);
                    this.form.Stockist.getGridField().toggle(false);
                    this.form.Distributor.getGridField().toggle(false);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "2") { //Customer from channel
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(true);
                    this.form.Distributor.getGridField().toggle(true);
                    this.form.Dealer.getGridField().toggle(true);
                    this.form.Wholesaler.getGridField().toggle(true);
                    this.form.Reseller.getGridField().toggle(true);
                }
                else if (this.form.ChannelCategory.value == "3") { //Reseller
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(true);
                    this.form.Distributor.getGridField().toggle(true);
                    this.form.Dealer.getGridField().toggle(true);
                    this.form.Wholesaler.getGridField().toggle(true);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "4") { //Wholesaler
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(true);
                    this.form.Distributor.getGridField().toggle(true);
                    this.form.Dealer.getGridField().toggle(true);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "5") { //Dealer 
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(true);
                    this.form.Distributor.getGridField().toggle(true);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "6") { //Distributor
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(true);
                    this.form.Distributor.getGridField().toggle(false);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "7") { //Stockist
                    this.form.NationalDistributor.getGridField().toggle(true);
                    this.form.Stockist.getGridField().toggle(false);
                    this.form.Distributor.getGridField().toggle(false);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
                else if (this.form.ChannelCategory.value == "8") {
                    this.form.NationalDistributor.getGridField().toggle(false);
                    this.form.Stockist.getGridField().toggle(false);
                    this.form.Distributor.getGridField().toggle(false);
                    this.form.Dealer.getGridField().toggle(false);
                    this.form.Wholesaler.getGridField().toggle(false);
                    this.form.Reseller.getGridField().toggle(false);
                }
            });


        }

        loadEntity(entity: ContactsRow) {
            super.loadEntity(entity);
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.Country.value < "1") {
                this.form.Country.value = Administration.CompanyDetailsRow.getLookup().itemById[1].Country.toString();
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].PhoneCompulsory == true) {
                this.form.Phone.element.addClass(" required");
            }
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].PincodeMandatory == true) {
            //    this.form.Pin.element.addClass(" required");
            //}
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].CountryMandatory == true) {
            //    this.form.Country.element.addClass(" required");
            //}
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].CityMandatory == true) {
            //    this.form.CityId.element.addClass(" required");
            //}
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].EmailCompulsory == true) {
            //    this.form.Email.element.addClass(" required");
            //}

            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].StateCompulsoryInContacts == true) {
            //    this.form.StateId.element.addClass(" required");
            //}

        }

        onSaveSuccess(response) {
            super.onSaveSuccess(response);

            Q.reloadLookupAsync('Contacts.Contacts');
            Q.reloadLookupAsync('Contacts.SubContacts');
        }

    }
}