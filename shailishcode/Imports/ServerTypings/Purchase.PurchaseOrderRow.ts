namespace AdvanceCRM.Purchase {
    export interface PurchaseOrderRow {
        Id?: number;
        ContactsId?: number;
        Date?: string;
        Status?: Masters.StatusMaster;
        Total?: number;
        Description?: string;
        AdditionalInfo?: string;
        SourceId?: number;
        BranchId?: number;
        Terms?: string;
        OwnerId?: number;
        AssignedId?: number;
        Attachments?: string;
        Roundup?: number;
        PurchaseOrderNo?: number;
        Lines?: number;
        Conversion?: number;
        CurrencyConversion?: boolean;
        FromCurrency?: Masters.Currencies;
        ToCurrency?: Masters.Currencies;
        ShippingAddress?: string;
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
        Source?: string;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        OwnerSource?: string;
        OwnerPasswordHash?: string;
        OwnerPasswordSalt?: string;
        OwnerLastDirectoryUpdate?: string;
        OwnerUserImage?: string;
        OwnerInsertDate?: string;
        OwnerInsertUserId?: number;
        OwnerUpdateDate?: string;
        OwnerUpdateUserId?: number;
        OwnerIsActive?: number;
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
        OwnerBranchId?: number;
        OwnerUid?: string;
        OwnerNonOperational?: boolean;
        AssignedUsername?: string;
        AssignedDisplayName?: string;
        AssignedEmail?: string;
        AssignedSource?: string;
        AssignedPasswordHash?: string;
        AssignedPasswordSalt?: string;
        AssignedLastDirectoryUpdate?: string;
        AssignedUserImage?: string;
        AssignedInsertDate?: string;
        AssignedInsertUserId?: number;
        AssignedUpdateDate?: string;
        AssignedUpdateUserId?: number;
        AssignedIsActive?: number;
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
        AssignedBranchId?: number;
        AssignedUid?: string;
        AssignedNonOperational?: boolean;
        DueDate?: string;
        Products?: PurchaseOrderProductsRow[];
        NoteList?: Common.NoteRow[];
        TermsList?: number[];
        QuotationNo?: number;
    }

    export namespace PurchaseOrderRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Description';
        export const localTextPrefix = 'Purchase.PurchaseOrder';
        export const lookupKey = 'Purchase.PurchaseOrderRow';

        export function getLookup(): Q.Lookup<PurchaseOrderRow> {
            return Q.getLookup<PurchaseOrderRow>('Purchase.PurchaseOrderRow');
        }
        export const deletePermission = 'PurchaseOrder:Delete';
        export const insertPermission = 'PurchaseOrder:Insert';
        export const readPermission = 'PurchaseOrder:Read';
        export const updatePermission = 'PurchaseOrder:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            Date = "Date",
            Status = "Status",
            Total = "Total",
            Description = "Description",
            AdditionalInfo = "AdditionalInfo",
            SourceId = "SourceId",
            BranchId = "BranchId",
            Terms = "Terms",
            OwnerId = "OwnerId",
            AssignedId = "AssignedId",
            Attachments = "Attachments",
            Roundup = "Roundup",
            PurchaseOrderNo = "PurchaseOrderNo",
            Lines = "Lines",
            Conversion = "Conversion",
            CurrencyConversion = "CurrencyConversion",
            FromCurrency = "FromCurrency",
            ToCurrency = "ToCurrency",
            ShippingAddress = "ShippingAddress",
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
            Source = "Source",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            OwnerSource = "OwnerSource",
            OwnerPasswordHash = "OwnerPasswordHash",
            OwnerPasswordSalt = "OwnerPasswordSalt",
            OwnerLastDirectoryUpdate = "OwnerLastDirectoryUpdate",
            OwnerUserImage = "OwnerUserImage",
            OwnerInsertDate = "OwnerInsertDate",
            OwnerInsertUserId = "OwnerInsertUserId",
            OwnerUpdateDate = "OwnerUpdateDate",
            OwnerUpdateUserId = "OwnerUpdateUserId",
            OwnerIsActive = "OwnerIsActive",
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
            OwnerBranchId = "OwnerBranchId",
            OwnerUid = "OwnerUid",
            OwnerNonOperational = "OwnerNonOperational",
            AssignedUsername = "AssignedUsername",
            AssignedDisplayName = "AssignedDisplayName",
            AssignedEmail = "AssignedEmail",
            AssignedSource = "AssignedSource",
            AssignedPasswordHash = "AssignedPasswordHash",
            AssignedPasswordSalt = "AssignedPasswordSalt",
            AssignedLastDirectoryUpdate = "AssignedLastDirectoryUpdate",
            AssignedUserImage = "AssignedUserImage",
            AssignedInsertDate = "AssignedInsertDate",
            AssignedInsertUserId = "AssignedInsertUserId",
            AssignedUpdateDate = "AssignedUpdateDate",
            AssignedUpdateUserId = "AssignedUpdateUserId",
            AssignedIsActive = "AssignedIsActive",
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
            AssignedBranchId = "AssignedBranchId",
            AssignedUid = "AssignedUid",
            AssignedNonOperational = "AssignedNonOperational",
            DueDate = "DueDate",
            Products = "Products",
            NoteList = "NoteList",
            TermsList = "TermsList",
            QuotationNo = "QuotationNo"
        }
    }
}
