namespace AdvanceCRM.Sales {
    export interface IndentRow {
        Id?: number;
        ContactsId?: number;
        Date?: string;
        Status?: Masters.StatusMaster;
        AdditionalInfo?: string;
        InvoiceNo?: string;
        BranchId?: number;
        OwnerId?: number;
        AssignedId?: number;
        Subject?: string;
        Reference?: string;
        ContactPersonId?: number;
        Lines?: number;
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
        ContactsCcEmails?: string;
        ContactsBccEmails?: string;
        ContactsAttachment?: string;
        ContactsEComGstin?: string;
        ContactsCreditorsOpening?: number;
        ContactsDebtorsOpening?: number;
        ContactsBankName?: string;
        ContactsAccountNumber?: string;
        ContactsIfsc?: string;
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
        ContactsWhatsapp?: string;
        ContactsConsentForCalling?: boolean;
        ContactsAdditionalInfo2?: string;
        ContactsDateCreated?: string;
        ContactsPassportNumber?: string;
        ContactsFirstName?: string;
        ContactsLastName?: string;
        ContactsExpiryDate?: string;
        ContactsAadharNo?: string;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        BranchCompanyId?: number;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        OwnerUpperLevel?: number;
        OwnerUpperLevel2?: number;
        OwnerUpperLevel3?: number;
        OwnerUpperLevel4?: number;
        OwnerUpperLevel5?: number;
        OwnerHost?: string;
        OwnerPort?: number;
        OwnerSsl?: boolean;
        OwnerEmailId?: string;
        OwnerEmailPassword?: string;
        OwnerPhone?: string;
        OwnerMcsmtpServer?: string;
        OwnerMcsmtpPort?: number;
        OwnerMcimapServer?: string;
        OwnerMcimapPort?: number;
        OwnerMcUsername?: string;
        OwnerMcPassword?: string;
        OwnerStartTime?: string;
        OwnerEndTime?: string;
        OwnerUid?: string;
        OwnerNonOperational?: boolean;
        OwnerBranchId?: number;
        OwnerCompanyId?: number;
        OwnerEnquiry?: boolean;
        OwnerQuotation?: boolean;
        OwnerTasks?: boolean;
        OwnerContacts?: boolean;
        OwnerPurchase?: boolean;
        OwnerSales?: boolean;
        OwnerCms?: boolean;
        OwnerLocation?: string;
        OwnerCoordinates?: string;
        OwnerTeamsId?: number;
        OwnerTenantId?: number;
        OwnerUrl?: string;
        OwnerPlan?: string;
        AssignedUsername?: string;
        AssignedDisplayName?: string;
        AssignedEmail?: string;
        AssignedUpperLevel?: number;
        AssignedUpperLevel2?: number;
        AssignedUpperLevel3?: number;
        AssignedUpperLevel4?: number;
        AssignedUpperLevel5?: number;
        AssignedHost?: string;
        AssignedPort?: number;
        AssignedSsl?: boolean;
        AssignedEmailId?: string;
        AssignedEmailPassword?: string;
        AssignedPhone?: string;
        AssignedMcsmtpServer?: string;
        AssignedMcsmtpPort?: number;
        AssignedMcimapServer?: string;
        AssignedMcimapPort?: number;
        AssignedMcUsername?: string;
        AssignedMcPassword?: string;
        AssignedStartTime?: string;
        AssignedEndTime?: string;
        AssignedUid?: string;
        AssignedNonOperational?: boolean;
        AssignedBranchId?: number;
        AssignedCompanyId?: number;
        AssignedEnquiry?: boolean;
        AssignedQuotation?: boolean;
        AssignedTasks?: boolean;
        AssignedContacts?: boolean;
        AssignedPurchase?: boolean;
        AssignedSales?: boolean;
        AssignedCms?: boolean;
        AssignedLocation?: string;
        AssignedCoordinates?: string;
        AssignedTeamsId?: number;
        AssignedTenantId?: number;
        AssignedUrl?: string;
        AssignedPlan?: string;
        ContactPersonName?: string;
        ContactPersonPhone?: string;
        ContactPersonResidentialPhone?: string;
        ContactPersonEmail?: string;
        ContactPersonDesignation?: string;
        ContactPersonAddress?: string;
        ContactPersonGender?: number;
        ContactPersonReligion?: number;
        ContactPersonMaritalStatus?: number;
        ContactPersonMarriageAnniversary?: string;
        ContactPersonBirthdate?: string;
        ContactPersonContactsId?: number;
        ContactPersonProject?: string;
        ContactPersonWhatsapp?: string;
        ContactPersonPassportNumber?: string;
        ContactPersonFirstName?: string;
        ContactPersonLastName?: string;
        ContactPersonExpiryDate?: string;
        ContactPersonAadharNo?: string;
        ContactPersonPanNo?: string;
        ContactPersonFileAttachments?: string;
        Products?: IndentProductsRow[];
    }

    export namespace IndentRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AdditionalInfo';
        export const localTextPrefix = 'Sales.Indent';
        export const lookupKey = 'Sales.Indent';

        export function getLookup(): Q.Lookup<IndentRow> {
            return Q.getLookup<IndentRow>('Sales.Indent');
        }
        export const deletePermission = 'Indent:Delete';
        export const insertPermission = 'Indent:Insert';
        export const readPermission = 'Indent:Read';
        export const updatePermission = 'Indent:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            Date = "Date",
            Status = "Status",
            AdditionalInfo = "AdditionalInfo",
            InvoiceNo = "InvoiceNo",
            BranchId = "BranchId",
            OwnerId = "OwnerId",
            AssignedId = "AssignedId",
            Subject = "Subject",
            Reference = "Reference",
            ContactPersonId = "ContactPersonId",
            Lines = "Lines",
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
            ContactsCcEmails = "ContactsCcEmails",
            ContactsBccEmails = "ContactsBccEmails",
            ContactsAttachment = "ContactsAttachment",
            ContactsEComGstin = "ContactsEComGstin",
            ContactsCreditorsOpening = "ContactsCreditorsOpening",
            ContactsDebtorsOpening = "ContactsDebtorsOpening",
            ContactsBankName = "ContactsBankName",
            ContactsAccountNumber = "ContactsAccountNumber",
            ContactsIfsc = "ContactsIfsc",
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
            ContactsWhatsapp = "ContactsWhatsapp",
            ContactsConsentForCalling = "ContactsConsentForCalling",
            ContactsAdditionalInfo2 = "ContactsAdditionalInfo2",
            ContactsDateCreated = "ContactsDateCreated",
            ContactsPassportNumber = "ContactsPassportNumber",
            ContactsFirstName = "ContactsFirstName",
            ContactsLastName = "ContactsLastName",
            ContactsExpiryDate = "ContactsExpiryDate",
            ContactsAadharNo = "ContactsAadharNo",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            BranchCompanyId = "BranchCompanyId",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            OwnerUpperLevel = "OwnerUpperLevel",
            OwnerUpperLevel2 = "OwnerUpperLevel2",
            OwnerUpperLevel3 = "OwnerUpperLevel3",
            OwnerUpperLevel4 = "OwnerUpperLevel4",
            OwnerUpperLevel5 = "OwnerUpperLevel5",
            OwnerHost = "OwnerHost",
            OwnerPort = "OwnerPort",
            OwnerSsl = "OwnerSsl",
            OwnerEmailId = "OwnerEmailId",
            OwnerEmailPassword = "OwnerEmailPassword",
            OwnerPhone = "OwnerPhone",
            OwnerMcsmtpServer = "OwnerMcsmtpServer",
            OwnerMcsmtpPort = "OwnerMcsmtpPort",
            OwnerMcimapServer = "OwnerMcimapServer",
            OwnerMcimapPort = "OwnerMcimapPort",
            OwnerMcUsername = "OwnerMcUsername",
            OwnerMcPassword = "OwnerMcPassword",
            OwnerStartTime = "OwnerStartTime",
            OwnerEndTime = "OwnerEndTime",
            OwnerUid = "OwnerUid",
            OwnerNonOperational = "OwnerNonOperational",
            OwnerBranchId = "OwnerBranchId",
            OwnerCompanyId = "OwnerCompanyId",
            OwnerEnquiry = "OwnerEnquiry",
            OwnerQuotation = "OwnerQuotation",
            OwnerTasks = "OwnerTasks",
            OwnerContacts = "OwnerContacts",
            OwnerPurchase = "OwnerPurchase",
            OwnerSales = "OwnerSales",
            OwnerCms = "OwnerCms",
            OwnerLocation = "OwnerLocation",
            OwnerCoordinates = "OwnerCoordinates",
            OwnerTeamsId = "OwnerTeamsId",
            OwnerTenantId = "OwnerTenantId",
            OwnerUrl = "OwnerUrl",
            OwnerPlan = "OwnerPlan",
            AssignedUsername = "AssignedUsername",
            AssignedDisplayName = "AssignedDisplayName",
            AssignedEmail = "AssignedEmail",
            AssignedUpperLevel = "AssignedUpperLevel",
            AssignedUpperLevel2 = "AssignedUpperLevel2",
            AssignedUpperLevel3 = "AssignedUpperLevel3",
            AssignedUpperLevel4 = "AssignedUpperLevel4",
            AssignedUpperLevel5 = "AssignedUpperLevel5",
            AssignedHost = "AssignedHost",
            AssignedPort = "AssignedPort",
            AssignedSsl = "AssignedSsl",
            AssignedEmailId = "AssignedEmailId",
            AssignedEmailPassword = "AssignedEmailPassword",
            AssignedPhone = "AssignedPhone",
            AssignedMcsmtpServer = "AssignedMcsmtpServer",
            AssignedMcsmtpPort = "AssignedMcsmtpPort",
            AssignedMcimapServer = "AssignedMcimapServer",
            AssignedMcimapPort = "AssignedMcimapPort",
            AssignedMcUsername = "AssignedMcUsername",
            AssignedMcPassword = "AssignedMcPassword",
            AssignedStartTime = "AssignedStartTime",
            AssignedEndTime = "AssignedEndTime",
            AssignedUid = "AssignedUid",
            AssignedNonOperational = "AssignedNonOperational",
            AssignedBranchId = "AssignedBranchId",
            AssignedCompanyId = "AssignedCompanyId",
            AssignedEnquiry = "AssignedEnquiry",
            AssignedQuotation = "AssignedQuotation",
            AssignedTasks = "AssignedTasks",
            AssignedContacts = "AssignedContacts",
            AssignedPurchase = "AssignedPurchase",
            AssignedSales = "AssignedSales",
            AssignedCms = "AssignedCms",
            AssignedLocation = "AssignedLocation",
            AssignedCoordinates = "AssignedCoordinates",
            AssignedTeamsId = "AssignedTeamsId",
            AssignedTenantId = "AssignedTenantId",
            AssignedUrl = "AssignedUrl",
            AssignedPlan = "AssignedPlan",
            ContactPersonName = "ContactPersonName",
            ContactPersonPhone = "ContactPersonPhone",
            ContactPersonResidentialPhone = "ContactPersonResidentialPhone",
            ContactPersonEmail = "ContactPersonEmail",
            ContactPersonDesignation = "ContactPersonDesignation",
            ContactPersonAddress = "ContactPersonAddress",
            ContactPersonGender = "ContactPersonGender",
            ContactPersonReligion = "ContactPersonReligion",
            ContactPersonMaritalStatus = "ContactPersonMaritalStatus",
            ContactPersonMarriageAnniversary = "ContactPersonMarriageAnniversary",
            ContactPersonBirthdate = "ContactPersonBirthdate",
            ContactPersonContactsId = "ContactPersonContactsId",
            ContactPersonProject = "ContactPersonProject",
            ContactPersonWhatsapp = "ContactPersonWhatsapp",
            ContactPersonPassportNumber = "ContactPersonPassportNumber",
            ContactPersonFirstName = "ContactPersonFirstName",
            ContactPersonLastName = "ContactPersonLastName",
            ContactPersonExpiryDate = "ContactPersonExpiryDate",
            ContactPersonAadharNo = "ContactPersonAadharNo",
            ContactPersonPanNo = "ContactPersonPanNo",
            ContactPersonFileAttachments = "ContactPersonFileAttachments",
            Products = "Products"
        }
    }
}
