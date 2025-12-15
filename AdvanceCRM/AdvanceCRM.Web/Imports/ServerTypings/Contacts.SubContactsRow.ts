namespace AdvanceCRM.Contacts {
    export interface SubContactsRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        ResidentialPhone?: string;
        Email?: string;
        Designation?: string;
        Address?: string;
        Gender?: Masters.GenderMaster;
        Religion?: Masters.ReligionMaster;
        MaritalStatus?: Masters.MaritalMaster;
        MarriageAnniversary?: string;
        Birthdate?: string;
        ContactsId?: number;
        Project?: string;
        Whatsapp?: string;
        MultiProjectList?: number[];
        ContactsContactType?: number;
        ContactsName?: string;
        ContactsPhone?: string;
        ContactsEmail?: string;
        ContactsAddress?: string;
        ContactsCityId?: number;
        ContactsStateId?: number;
        ContactsPin?: string;
        ContactsCountry?: number;
        ContactsWebsite?: string;
        ContactsAdditionalInfo?: string;
        ContactsResidentialPhone?: string;
        ContactsOfficePhone?: string;
        ContactsGender?: number;
        ContactsReligion?: number;
        ContactsAreaId?: number;
        ContactsMaritalStatus?: number;
        ContactsMarriageAnniversary?: string;
        ContactsBirthdate?: string;
        ContactsDateOfIncorporation?: string;
        ContactsCategoryId?: number;
        ContactsGradeId?: number;
        ContactsType?: number;
        ContactsOwnerId?: number;
        ContactsAssignedId?: number;
        ContactsChannelCategory?: number;
        ContactsNationalDistributor?: number;
        ContactsStockist?: number;
        ContactsDistributor?: number;
        ContactsDealer?: number;
        ContactsWholesaler?: number;
        ContactsReseller?: number;
        ContactsGstin?: string;
        ContactsPanNo?: string;
        ContactsCCEmails?: string;
        ContactsBCCEmails?: string;
        ContactsAttachment?: string;
        ContactsEComGSTIN?: string;
        ContactsCreditorsOpening?: number;
        ContactsDebtorsOpening?: number;
        ContactsBankName?: string;
        ContactsAccountNumber?: string;
        ContactsIFSC?: string;
        ContactsBankType?: string;
        ContactsBranch?: string;
        ContactsAccountsEmail?: string;
        ContactsPurchaseEmail?: string;
        ContactsServiceEmail?: string;
        ContactsSalesEmail?: string;
        ContactsCreditDays?: number;
        ContactsCustomerType?: number;
        ContactsTrasportationId?: number;
        ContactsTehsilId?: number;
        ContactsVillageId?: number;
        NamePlusPhone?: string;
        PassportNumber?: string;
        FirstName?: string;
        LastName?: string;
        ExpiryDate?: string;
        PANNo?: string;
        AadharNo?: string;
        FileAttachments?: string;
    }

    export namespace SubContactsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Contacts.SubContacts';
        export const lookupKey = 'Contacts.SubContacts';

        export function getLookup(): Q.Lookup<SubContactsRow> {
            return Q.getLookup<SubContactsRow>('Contacts.SubContacts');
        }
        export const deletePermission = 'Contacts:Delete';
        export const insertPermission = 'Contacts:Insert';
        export const readPermission = 'Contacts:Read';
        export const updatePermission = 'Contacts:Update';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Phone = "Phone",
            ResidentialPhone = "ResidentialPhone",
            Email = "Email",
            Designation = "Designation",
            Address = "Address",
            Gender = "Gender",
            Religion = "Religion",
            MaritalStatus = "MaritalStatus",
            MarriageAnniversary = "MarriageAnniversary",
            Birthdate = "Birthdate",
            ContactsId = "ContactsId",
            Project = "Project",
            Whatsapp = "Whatsapp",
            MultiProjectList = "MultiProjectList",
            ContactsContactType = "ContactsContactType",
            ContactsName = "ContactsName",
            ContactsPhone = "ContactsPhone",
            ContactsEmail = "ContactsEmail",
            ContactsAddress = "ContactsAddress",
            ContactsCityId = "ContactsCityId",
            ContactsStateId = "ContactsStateId",
            ContactsPin = "ContactsPin",
            ContactsCountry = "ContactsCountry",
            ContactsWebsite = "ContactsWebsite",
            ContactsAdditionalInfo = "ContactsAdditionalInfo",
            ContactsResidentialPhone = "ContactsResidentialPhone",
            ContactsOfficePhone = "ContactsOfficePhone",
            ContactsGender = "ContactsGender",
            ContactsReligion = "ContactsReligion",
            ContactsAreaId = "ContactsAreaId",
            ContactsMaritalStatus = "ContactsMaritalStatus",
            ContactsMarriageAnniversary = "ContactsMarriageAnniversary",
            ContactsBirthdate = "ContactsBirthdate",
            ContactsDateOfIncorporation = "ContactsDateOfIncorporation",
            ContactsCategoryId = "ContactsCategoryId",
            ContactsGradeId = "ContactsGradeId",
            ContactsType = "ContactsType",
            ContactsOwnerId = "ContactsOwnerId",
            ContactsAssignedId = "ContactsAssignedId",
            ContactsChannelCategory = "ContactsChannelCategory",
            ContactsNationalDistributor = "ContactsNationalDistributor",
            ContactsStockist = "ContactsStockist",
            ContactsDistributor = "ContactsDistributor",
            ContactsDealer = "ContactsDealer",
            ContactsWholesaler = "ContactsWholesaler",
            ContactsReseller = "ContactsReseller",
            ContactsGstin = "ContactsGstin",
            ContactsPanNo = "ContactsPanNo",
            ContactsCCEmails = "ContactsCCEmails",
            ContactsBCCEmails = "ContactsBCCEmails",
            ContactsAttachment = "ContactsAttachment",
            ContactsEComGSTIN = "ContactsEComGSTIN",
            ContactsCreditorsOpening = "ContactsCreditorsOpening",
            ContactsDebtorsOpening = "ContactsDebtorsOpening",
            ContactsBankName = "ContactsBankName",
            ContactsAccountNumber = "ContactsAccountNumber",
            ContactsIFSC = "ContactsIFSC",
            ContactsBankType = "ContactsBankType",
            ContactsBranch = "ContactsBranch",
            ContactsAccountsEmail = "ContactsAccountsEmail",
            ContactsPurchaseEmail = "ContactsPurchaseEmail",
            ContactsServiceEmail = "ContactsServiceEmail",
            ContactsSalesEmail = "ContactsSalesEmail",
            ContactsCreditDays = "ContactsCreditDays",
            ContactsCustomerType = "ContactsCustomerType",
            ContactsTrasportationId = "ContactsTrasportationId",
            ContactsTehsilId = "ContactsTehsilId",
            ContactsVillageId = "ContactsVillageId",
            NamePlusPhone = "NamePlusPhone",
            PassportNumber = "PassportNumber",
            FirstName = "FirstName",
            LastName = "LastName",
            ExpiryDate = "ExpiryDate",
            PANNo = "PANNo",
            AadharNo = "AadharNo",
            FileAttachments = "FileAttachments"
        }
    }
}
