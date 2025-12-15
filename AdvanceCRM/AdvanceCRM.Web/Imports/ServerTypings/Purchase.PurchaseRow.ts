namespace AdvanceCRM.Purchase {
    export interface PurchaseRow {
        Id?: number;
        InvoiceNo?: string;
        PurchaseFromId?: number;
        InvoiceDate?: string;
        Total?: number;
        Status?: Masters.StatusMaster;
        Type?: Masters.InvoiceTypeMaster;
        AdditionalInfo?: string;
        BranchId?: number;
        OwnerId?: number;
        AssignedId?: number;
        ReverseCharge?: boolean;
        InvoiceType?: Masters.GSTInvoiceTypeMaster;
        ITCEligibility?: Masters.GSTITCEligibilityTypeMaster;
        Attachments?: string;
        Roundup?: number;
        PurchaseFromContactType?: number;
        PurchaseFromName?: string;
        PurchaseFromPhone?: string;
        PurchaseFromEmail?: string;
        PurchaseFromAddress?: string;
        PurchaseFromCityId?: number;
        PurchaseFromStateId?: number;
        PurchaseFromPin?: string;
        PurchaseFromCountry?: number;
        PurchaseFromWebsite?: string;
        PurchaseFromAdditionalInfo?: string;
        PurchaseFromResidentialPhone?: string;
        PurchaseFromOfficePhone?: string;
        PurchaseFromGender?: number;
        PurchaseFromReligion?: number;
        PurchaseFromAreaId?: number;
        PurchaseFromMaritalStatus?: number;
        PurchaseFromMarriageAnniversary?: string;
        PurchaseFromBirthdate?: string;
        PurchaseFromDateOfIncorporation?: string;
        PurchaseFromCategoryId?: number;
        PurchaseFromGradeId?: number;
        PurchaseFromType?: number;
        PurchaseFromOwnerId?: number;
        PurchaseFromAssignedId?: number;
        PurchaseFromChannelCategory?: number;
        PurchaseFromNationalDistributor?: number;
        PurchaseFromStockist?: number;
        PurchaseFromDistributor?: number;
        PurchaseFromDealer?: number;
        PurchaseFromWholesaler?: number;
        PurchaseFromReseller?: number;
        PurchaseFromGSTIN?: string;
        PurchaseFromPanNo?: string;
        PurchaseFromCcEmails?: string;
        PurchaseFromBccEmails?: string;
        PurchaseFromAttachment?: string;
        PurchaseFromEComGSTIN?: string;
        PurchaseFromCreditorsOpening?: number;
        PurchaseFromDebtorsOpening?: number;
        PurchaseFromBankName?: string;
        PurchaseFromAccountNumber?: string;
        PurchaseFromIfsc?: string;
        PurchaseFromBankType?: string;
        PurchaseFromBranch?: string;
        PurchaseFromAccountsEmail?: string;
        PurchaseFromPurchaseEmail?: string;
        PurchaseFromServiceEmail?: string;
        PurchaseFromSalesEmail?: string;
        PurchaseFromCreditDays?: number;
        PurchaseFromCustomerType?: number;
        PurchaseFromTrasportationId?: number;
        PurchaseFromTehsilId?: number;
        PurchaseFromVillageId?: number;
        PurchaseFromWhatsapp?: string;
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
        ApprovedByUsername?: string;
        ApprovedByDisplayName?: string;
        ApprovedByEmail?: string;
        ApprovedBySource?: string;
        ApprovedByPasswordHash?: string;
        ApprovedByPasswordSalt?: string;
        ApprovedByLastDirectoryUpdate?: string;
        ApprovedByUserImage?: string;
        ApprovedByInsertDate?: string;
        ApprovedByInsertUserId?: number;
        ApprovedByUpdateDate?: string;
        ApprovedByUpdateUserId?: number;
        ApprovedByIsActive?: number;
        ApprovedByUpperLevel?: number;
        ApprovedByUpperLevel2?: number;
        ApprovedByUpperLevel3?: number;
        ApprovedByUpperLevel4?: number;
        ApprovedByUpperLevel5?: number;
        ApprovedByHost?: string;
        ApprovedByPort?: number;
        ApprovedBySsl?: boolean;
        ApprovedByEmailId?: string;
        ApprovedByEmailPassword?: string;
        ApprovedByPhone?: string;
        ApprovedByMcsmtpServer?: string;
        ApprovedByMcsmtpPort?: number;
        ApprovedByMcimapServer?: string;
        ApprovedByMcimapPort?: number;
        ApprovedByMcUsername?: string;
        ApprovedByMcPassword?: string;
        ApprovedByStartTime?: string;
        ApprovedByEndTime?: string;
        ApprovedByBranchId?: number;
        ApprovedByUid?: string;
        ApprovedByNonOperational?: boolean;
        ApprovedBy?: number;
        Products?: PurchaseProductsRow[];
        NoteList?: Common.NoteRow[];
        OwnerTeamsId?: number;
        AssignedTeamsId?: number;
        CompanyId?: number;
        QuotationNo?: number;
    }

    export namespace PurchaseRow {
        export const idProperty = 'Id';
        export const nameProperty = 'InvoiceNo';
        export const localTextPrefix = 'Purchase.Purchase';
        export const lookupKey = 'Purchase.PurchaseRow';

        export function getLookup(): Q.Lookup<PurchaseRow> {
            return Q.getLookup<PurchaseRow>('Purchase.PurchaseRow');
        }
        export const deletePermission = 'Purchase:Delete';
        export const insertPermission = 'Purchase:Insert';
        export const readPermission = 'Purchase:Read';
        export const updatePermission = 'Purchase:Update';

        export declare const enum Fields {
            Id = "Id",
            InvoiceNo = "InvoiceNo",
            PurchaseFromId = "PurchaseFromId",
            InvoiceDate = "InvoiceDate",
            Total = "Total",
            Status = "Status",
            Type = "Type",
            AdditionalInfo = "AdditionalInfo",
            BranchId = "BranchId",
            OwnerId = "OwnerId",
            AssignedId = "AssignedId",
            ReverseCharge = "ReverseCharge",
            InvoiceType = "InvoiceType",
            ITCEligibility = "ITCEligibility",
            Attachments = "Attachments",
            Roundup = "Roundup",
            PurchaseFromContactType = "PurchaseFromContactType",
            PurchaseFromName = "PurchaseFromName",
            PurchaseFromPhone = "PurchaseFromPhone",
            PurchaseFromEmail = "PurchaseFromEmail",
            PurchaseFromAddress = "PurchaseFromAddress",
            PurchaseFromCityId = "PurchaseFromCityId",
            PurchaseFromStateId = "PurchaseFromStateId",
            PurchaseFromPin = "PurchaseFromPin",
            PurchaseFromCountry = "PurchaseFromCountry",
            PurchaseFromWebsite = "PurchaseFromWebsite",
            PurchaseFromAdditionalInfo = "PurchaseFromAdditionalInfo",
            PurchaseFromResidentialPhone = "PurchaseFromResidentialPhone",
            PurchaseFromOfficePhone = "PurchaseFromOfficePhone",
            PurchaseFromGender = "PurchaseFromGender",
            PurchaseFromReligion = "PurchaseFromReligion",
            PurchaseFromAreaId = "PurchaseFromAreaId",
            PurchaseFromMaritalStatus = "PurchaseFromMaritalStatus",
            PurchaseFromMarriageAnniversary = "PurchaseFromMarriageAnniversary",
            PurchaseFromBirthdate = "PurchaseFromBirthdate",
            PurchaseFromDateOfIncorporation = "PurchaseFromDateOfIncorporation",
            PurchaseFromCategoryId = "PurchaseFromCategoryId",
            PurchaseFromGradeId = "PurchaseFromGradeId",
            PurchaseFromType = "PurchaseFromType",
            PurchaseFromOwnerId = "PurchaseFromOwnerId",
            PurchaseFromAssignedId = "PurchaseFromAssignedId",
            PurchaseFromChannelCategory = "PurchaseFromChannelCategory",
            PurchaseFromNationalDistributor = "PurchaseFromNationalDistributor",
            PurchaseFromStockist = "PurchaseFromStockist",
            PurchaseFromDistributor = "PurchaseFromDistributor",
            PurchaseFromDealer = "PurchaseFromDealer",
            PurchaseFromWholesaler = "PurchaseFromWholesaler",
            PurchaseFromReseller = "PurchaseFromReseller",
            PurchaseFromGSTIN = "PurchaseFromGSTIN",
            PurchaseFromPanNo = "PurchaseFromPanNo",
            PurchaseFromCcEmails = "PurchaseFromCcEmails",
            PurchaseFromBccEmails = "PurchaseFromBccEmails",
            PurchaseFromAttachment = "PurchaseFromAttachment",
            PurchaseFromEComGSTIN = "PurchaseFromEComGSTIN",
            PurchaseFromCreditorsOpening = "PurchaseFromCreditorsOpening",
            PurchaseFromDebtorsOpening = "PurchaseFromDebtorsOpening",
            PurchaseFromBankName = "PurchaseFromBankName",
            PurchaseFromAccountNumber = "PurchaseFromAccountNumber",
            PurchaseFromIfsc = "PurchaseFromIfsc",
            PurchaseFromBankType = "PurchaseFromBankType",
            PurchaseFromBranch = "PurchaseFromBranch",
            PurchaseFromAccountsEmail = "PurchaseFromAccountsEmail",
            PurchaseFromPurchaseEmail = "PurchaseFromPurchaseEmail",
            PurchaseFromServiceEmail = "PurchaseFromServiceEmail",
            PurchaseFromSalesEmail = "PurchaseFromSalesEmail",
            PurchaseFromCreditDays = "PurchaseFromCreditDays",
            PurchaseFromCustomerType = "PurchaseFromCustomerType",
            PurchaseFromTrasportationId = "PurchaseFromTrasportationId",
            PurchaseFromTehsilId = "PurchaseFromTehsilId",
            PurchaseFromVillageId = "PurchaseFromVillageId",
            PurchaseFromWhatsapp = "PurchaseFromWhatsapp",
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
            ApprovedByUsername = "ApprovedByUsername",
            ApprovedByDisplayName = "ApprovedByDisplayName",
            ApprovedByEmail = "ApprovedByEmail",
            ApprovedBySource = "ApprovedBySource",
            ApprovedByPasswordHash = "ApprovedByPasswordHash",
            ApprovedByPasswordSalt = "ApprovedByPasswordSalt",
            ApprovedByLastDirectoryUpdate = "ApprovedByLastDirectoryUpdate",
            ApprovedByUserImage = "ApprovedByUserImage",
            ApprovedByInsertDate = "ApprovedByInsertDate",
            ApprovedByInsertUserId = "ApprovedByInsertUserId",
            ApprovedByUpdateDate = "ApprovedByUpdateDate",
            ApprovedByUpdateUserId = "ApprovedByUpdateUserId",
            ApprovedByIsActive = "ApprovedByIsActive",
            ApprovedByUpperLevel = "ApprovedByUpperLevel",
            ApprovedByUpperLevel2 = "ApprovedByUpperLevel2",
            ApprovedByUpperLevel3 = "ApprovedByUpperLevel3",
            ApprovedByUpperLevel4 = "ApprovedByUpperLevel4",
            ApprovedByUpperLevel5 = "ApprovedByUpperLevel5",
            ApprovedByHost = "ApprovedByHost",
            ApprovedByPort = "ApprovedByPort",
            ApprovedBySsl = "ApprovedBySsl",
            ApprovedByEmailId = "ApprovedByEmailId",
            ApprovedByEmailPassword = "ApprovedByEmailPassword",
            ApprovedByPhone = "ApprovedByPhone",
            ApprovedByMcsmtpServer = "ApprovedByMcsmtpServer",
            ApprovedByMcsmtpPort = "ApprovedByMcsmtpPort",
            ApprovedByMcimapServer = "ApprovedByMcimapServer",
            ApprovedByMcimapPort = "ApprovedByMcimapPort",
            ApprovedByMcUsername = "ApprovedByMcUsername",
            ApprovedByMcPassword = "ApprovedByMcPassword",
            ApprovedByStartTime = "ApprovedByStartTime",
            ApprovedByEndTime = "ApprovedByEndTime",
            ApprovedByBranchId = "ApprovedByBranchId",
            ApprovedByUid = "ApprovedByUid",
            ApprovedByNonOperational = "ApprovedByNonOperational",
            ApprovedBy = "ApprovedBy",
            Products = "Products",
            NoteList = "NoteList",
            OwnerTeamsId = "OwnerTeamsId",
            AssignedTeamsId = "AssignedTeamsId",
            CompanyId = "CompanyId",
            QuotationNo = "QuotationNo"
        }
    }
}
