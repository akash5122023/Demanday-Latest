namespace AdvanceCRM.Contacts {
    export interface ContactsRow {
        Id?: number;
        ContactType?: Masters.CTypeMaster;
        Name?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        CityId?: number;
        StateId?: number;
        Pin?: string;
        Country?: Masters.CountryMaster;
        Website?: string;
        AdditionalInfo?: string;
        ResidentialPhone?: string;
        OfficePhone?: string;
        Gender?: Masters.GenderMaster;
        Religion?: Masters.ReligionMaster;
        AreaId?: number;
        MaritalStatus?: Masters.MaritalMaster;
        MarriageAnniversary?: string;
        Birthdate?: string;
        DateOfIncorporation?: string;
        CategoryId?: number;
        GradeId?: number;
        Type?: Masters.TypeMaster;
        OwnerId?: number;
        AssignedId?: number;
        ChannelCategory?: Masters.ChannelCategory;
        NationalDistributor?: number;
        Stockist?: number;
        Distributor?: number;
        Dealer?: number;
        Wholesaler?: number;
        Reseller?: number;
        GSTIN?: string;
        PANNo?: string;
        CCEmails?: string;
        BCCEmails?: string;
        Attachment?: string;
        EComGSTIN?: string;
        CreditorsOpening?: number;
        DebtorsOpening?: number;
        BankName?: string;
        AccountNumber?: string;
        IFSC?: string;
        BankType?: string;
        Branch?: string;
        AccountsEmail?: string;
        PurchaseEmail?: string;
        ServiceEmail?: string;
        SalesEmail?: string;
        CreditDays?: number;
        CustomerType?: Masters.ContactTypeMaster;
        TrasportationId?: number;
        TehsilId?: number;
        VillageId?: number;
        Whatsapp?: string;
        AdditionalInfo2?: string;
        DateCreated?: string;
        City?: string;
        CityStateId?: number;
        State?: string;
        Area?: string;
        Category?: string;
        CategoryType?: number;
        Grade?: string;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        AssignedUsername?: string;
        AssignedDisplayName?: string;
        AssignedEmail?: string;
        Tehsil?: string;
        TehsilStateId?: number;
        TehsilCityId?: number;
        Village?: string;
        VillageStateId?: number;
        VillageCityId?: number;
        VillageTehsilId?: number;
        SubContacts?: SubContactsRow[];
        MultiAssignList?: number[];
        ContactAddinfoList?: number[];
        NoteList?: Common.NoteRow[];
        PassportNumber?: string;
        FirstName?: string;
        LastName?: string;
        ExpiryDate?: string;
        AadharNo?: string;
    }

    export namespace ContactsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Contacts.Contacts';
        export const lookupKey = 'Contacts.Contacts';

        export function getLookup(): Q.Lookup<ContactsRow> {
            return Q.getLookup<ContactsRow>('Contacts.Contacts');
        }
        export const deletePermission = 'Contacts:Delete';
        export const insertPermission = 'Contacts:Insert';
        export const readPermission = 'Contacts:Read';
        export const updatePermission = 'Contacts:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactType = "ContactType",
            Name = "Name",
            Phone = "Phone",
            Email = "Email",
            Address = "Address",
            CityId = "CityId",
            StateId = "StateId",
            Pin = "Pin",
            Country = "Country",
            Website = "Website",
            AdditionalInfo = "AdditionalInfo",
            ResidentialPhone = "ResidentialPhone",
            OfficePhone = "OfficePhone",
            Gender = "Gender",
            Religion = "Religion",
            AreaId = "AreaId",
            MaritalStatus = "MaritalStatus",
            MarriageAnniversary = "MarriageAnniversary",
            Birthdate = "Birthdate",
            DateOfIncorporation = "DateOfIncorporation",
            CategoryId = "CategoryId",
            GradeId = "GradeId",
            Type = "Type",
            OwnerId = "OwnerId",
            AssignedId = "AssignedId",
            ChannelCategory = "ChannelCategory",
            NationalDistributor = "NationalDistributor",
            Stockist = "Stockist",
            Distributor = "Distributor",
            Dealer = "Dealer",
            Wholesaler = "Wholesaler",
            Reseller = "Reseller",
            GSTIN = "GSTIN",
            PANNo = "PANNo",
            CCEmails = "CCEmails",
            BCCEmails = "BCCEmails",
            Attachment = "Attachment",
            EComGSTIN = "EComGSTIN",
            CreditorsOpening = "CreditorsOpening",
            DebtorsOpening = "DebtorsOpening",
            BankName = "BankName",
            AccountNumber = "AccountNumber",
            IFSC = "IFSC",
            BankType = "BankType",
            Branch = "Branch",
            AccountsEmail = "AccountsEmail",
            PurchaseEmail = "PurchaseEmail",
            ServiceEmail = "ServiceEmail",
            SalesEmail = "SalesEmail",
            CreditDays = "CreditDays",
            CustomerType = "CustomerType",
            TrasportationId = "TrasportationId",
            TehsilId = "TehsilId",
            VillageId = "VillageId",
            Whatsapp = "Whatsapp",
            AdditionalInfo2 = "AdditionalInfo2",
            DateCreated = "DateCreated",
            City = "City",
            CityStateId = "CityStateId",
            State = "State",
            Area = "Area",
            Category = "Category",
            CategoryType = "CategoryType",
            Grade = "Grade",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            AssignedUsername = "AssignedUsername",
            AssignedDisplayName = "AssignedDisplayName",
            AssignedEmail = "AssignedEmail",
            Tehsil = "Tehsil",
            TehsilStateId = "TehsilStateId",
            TehsilCityId = "TehsilCityId",
            Village = "Village",
            VillageStateId = "VillageStateId",
            VillageCityId = "VillageCityId",
            VillageTehsilId = "VillageTehsilId",
            SubContacts = "SubContacts",
            MultiAssignList = "MultiAssignList",
            ContactAddinfoList = "ContactAddinfoList",
            NoteList = "NoteList",
            PassportNumber = "PassportNumber",
            FirstName = "FirstName",
            LastName = "LastName",
            ExpiryDate = "ExpiryDate",
            AadharNo = "AadharNo"
        }
    }
}
