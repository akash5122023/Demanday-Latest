namespace AdvanceCRM.Contacts {
    export interface ContactsForm {
        ContactType: Serenity.EnumEditor;
        DateCreated: Serenity.DateTimeEditor;
        CustomerType: Serenity.EnumEditor;
        CategoryId: Serenity.LookupEditor;
        GradeId: Serenity.LookupEditor;
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        Address: Serenity.TextAreaEditor;
        Country: Serenity.EnumEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
        TehsilId: Serenity.LookupEditor;
        VillageId: Serenity.LookupEditor;
        Whatsapp: Serenity.StringEditor;
        Pin: Serenity.StringEditor;
        Website: Serenity.StringEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        AdditionalInfo2: Serenity.TextAreaEditor;
        ContactAddinfoList: Serenity.LookupEditor;
        SubContacts: SubContactsEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        MultiAssignList: Serenity.LookupEditor;
        GSTIN: Serenity.StringEditor;
        EComGSTIN: Serenity.StringEditor;
        PANNo: Serenity.StringEditor;
        AadharNo: Serenity.StringEditor;
        ResidentialPhone: Serenity.MaskedEditor;
        OfficePhone: Serenity.MaskedEditor;
        Gender: Serenity.EnumEditor;
        Religion: Serenity.EnumEditor;
        Type: Serenity.EnumEditor;
        AreaId: Serenity.LookupEditor;
        TrasportationId: Serenity.LookupEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        CreditorsOpening: Serenity.DecimalEditor;
        DebtorsOpening: Serenity.DecimalEditor;
        CreditDays: Serenity.IntegerEditor;
        PassportNumber: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        ExpiryDate: Serenity.DateEditor;
        MaritalStatus: Serenity.EnumEditor;
        MarriageAnniversary: Serenity.DateEditor;
        Birthdate: Serenity.DateEditor;
        DateOfIncorporation: Serenity.DateEditor;
        AccountsEmail: Serenity.EmailEditor;
        PurchaseEmail: Serenity.EmailEditor;
        SalesEmail: Serenity.EmailEditor;
        ServiceEmail: Serenity.EmailEditor;
        CCEmails: Serenity.TextAreaEditor;
        BCCEmails: Serenity.TextAreaEditor;
        ChannelCategory: Serenity.EnumEditor;
        NationalDistributor: Serenity.LookupEditor;
        Stockist: Serenity.LookupEditor;
        Distributor: Serenity.LookupEditor;
        Dealer: Serenity.LookupEditor;
        Wholesaler: Serenity.LookupEditor;
        Reseller: Serenity.LookupEditor;
        BankName: Serenity.StringEditor;
        AccountNumber: Serenity.StringEditor;
        IFSC: Serenity.StringEditor;
        BankType: Serenity.StringEditor;
        Branch: Serenity.StringEditor;
        NoteList: Common.NotesEditor;
    }

    export class ContactsForm extends Serenity.PrefixedContext {
        static formKey = 'Contacts.Contacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactsForm.init)  {
                ContactsForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.LookupEditor;
                var w3 = s.StringEditor;
                var w4 = s.EmailEditor;
                var w5 = s.TextAreaEditor;
                var w6 = SubContactsEditor;
                var w7 = Administration.UserEditor;
                var w8 = s.MaskedEditor;
                var w9 = s.MultipleImageUploadEditor;
                var w10 = s.DecimalEditor;
                var w11 = s.IntegerEditor;
                var w12 = s.DateEditor;
                var w13 = Common.NotesEditor;

                Q.initFormType(ContactsForm, [
                    'ContactType', w0,
                    'DateCreated', w1,
                    'CustomerType', w0,
                    'CategoryId', w2,
                    'GradeId', w2,
                    'Name', w3,
                    'Phone', w3,
                    'Email', w4,
                    'Address', w5,
                    'Country', w0,
                    'StateId', w2,
                    'CityId', w2,
                    'TehsilId', w2,
                    'VillageId', w2,
                    'Whatsapp', w3,
                    'Pin', w3,
                    'Website', w3,
                    'AdditionalInfo', w5,
                    'AdditionalInfo2', w5,
                    'ContactAddinfoList', w2,
                    'SubContacts', w6,
                    'OwnerId', w7,
                    'AssignedId', w7,
                    'MultiAssignList', w2,
                    'GSTIN', w3,
                    'EComGSTIN', w3,
                    'PANNo', w3,
                    'AadharNo', w3,
                    'ResidentialPhone', w8,
                    'OfficePhone', w8,
                    'Gender', w0,
                    'Religion', w0,
                    'Type', w0,
                    'AreaId', w2,
                    'TrasportationId', w2,
                    'Attachment', w9,
                    'CreditorsOpening', w10,
                    'DebtorsOpening', w10,
                    'CreditDays', w11,
                    'PassportNumber', w3,
                    'FirstName', w3,
                    'LastName', w3,
                    'ExpiryDate', w12,
                    'MaritalStatus', w0,
                    'MarriageAnniversary', w12,
                    'Birthdate', w12,
                    'DateOfIncorporation', w12,
                    'AccountsEmail', w4,
                    'PurchaseEmail', w4,
                    'SalesEmail', w4,
                    'ServiceEmail', w4,
                    'CCEmails', w5,
                    'BCCEmails', w5,
                    'ChannelCategory', w0,
                    'NationalDistributor', w2,
                    'Stockist', w2,
                    'Distributor', w2,
                    'Dealer', w2,
                    'Wholesaler', w2,
                    'Reseller', w2,
                    'BankName', w3,
                    'AccountNumber', w3,
                    'IFSC', w3,
                    'BankType', w3,
                    'Branch', w3,
                    'NoteList', w13
                ]);
            }
        }
    }
}
