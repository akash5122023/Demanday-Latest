namespace AdvanceCRM.Accounting {
    export interface CashbookRow {
        Id?: number;
        Date?: string;
        Type?: Masters.TransactionTypeMaster;
        Head?: number;
        ContactsId?: number;
        InvoiceNo?: string;
        CashIn?: number;
        CashOut?: number;
        Narration?: string;
        BankId?: number;
        Head1?: string;
        HeadType?: number;
        CompanyId?: number;
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
        BankBankName?: string;
        BankAccountNumber?: string;
        BankIfsc?: string;
        BankType?: string;
        BankBranch?: string;
        BankAdditionalInfo?: string;
        ProjectId?: number;
        ProjectName?: string;
        EmployeeId?: number;
        EmployeeEmpCode?: string;
        EmployeeDepartmentId?: number;
        EmployeeName?: string;
        EmployeePhone?: string;
        EmployeeEmail?: string;
        EmployeeAddress?: string;
        EmployeeProfessionalEmail?: string;
        EmployeeCityId?: number;
        EmployeeStateId?: number;
        EmployeePin?: string;
        EmployeeCountry?: number;
        EmployeeAdditionalInfo?: string;
        EmployeeGender?: number;
        EmployeeReligion?: number;
        EmployeeAreaId?: number;
        EmployeeMaritalStatus?: number;
        EmployeeMarriageAnniversary?: string;
        EmployeeBirthdate?: string;
        EmployeeDateOfJoining?: string;
        EmployeeCompanyId?: number;
        EmployeeRolesId?: number;
        EmployeeOwnerId?: number;
        EmployeeAdhaarNo?: string;
        EmployeePanNo?: string;
        EmployeeAttachment?: string;
        EmployeeBankName?: string;
        EmployeeAccountNumber?: string;
        EmployeeIfsc?: string;
        EmployeeBankType?: string;
        EmployeeBranch?: string;
        EmployeeTehsilId?: number;
        EmployeeVillageId?: number;
        ProjectAmtIn?: number;
        IsCashIn?: boolean;
        Purpose?: string;
        RepresentativeId?: number;
        RepresentativeUsername?: string;
        RepresentativeDisplayName?: string;
        RepresentativeEmail?: string;
        RepresentativeSource?: string;
        RepresentativePasswordHash?: string;
        RepresentativePasswordSalt?: string;
        RepresentativeLastDirectoryUpdate?: string;
        RepresentativeUserImage?: string;
        RepresentativeInsertDate?: string;
        RepresentativeInsertUserId?: number;
        RepresentativeUpdateDate?: string;
        RepresentativeUpdateUserId?: number;
        RepresentativeIsActive?: number;
        RepresentativeUpperLevel?: number;
        RepresentativeUpperLevel2?: number;
        RepresentativeUpperLevel3?: number;
        RepresentativeUpperLevel4?: number;
        RepresentativeUpperLevel5?: number;
        RepresentativeHost?: string;
        RepresentativePort?: number;
        RepresentativeSsl?: boolean;
        RepresentativeEmailId?: string;
        RepresentativeEmailPassword?: string;
        RepresentativePhone?: string;
        RepresentativeMcsmtpServer?: string;
        RepresentativeMcsmtpPort?: number;
        RepresentativeMcimapServer?: string;
        RepresentativeMcimapPort?: number;
        RepresentativeMcUsername?: string;
        RepresentativeMcPassword?: string;
        RepresentativeStartTime?: string;
        RepresentativeEndTime?: string;
        RepresentativeBranchId?: number;
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
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
    }

    export namespace CashbookRow {
        export const idProperty = 'Id';
        export const nameProperty = 'InvoiceNo';
        export const localTextPrefix = 'Accounting.Cashbook';
        export const deletePermission = 'Cashbook:Delete';
        export const insertPermission = 'Cashbook:Insert';
        export const readPermission = 'Cashbook:Read';
        export const updatePermission = 'Cashbook:Update';

        export declare const enum Fields {
            Id = "Id",
            Date = "Date",
            Type = "Type",
            Head = "Head",
            ContactsId = "ContactsId",
            InvoiceNo = "InvoiceNo",
            CashIn = "CashIn",
            CashOut = "CashOut",
            Narration = "Narration",
            BankId = "BankId",
            Head1 = "Head1",
            HeadType = "HeadType",
            CompanyId = "CompanyId",
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
            BankBankName = "BankBankName",
            BankAccountNumber = "BankAccountNumber",
            BankIfsc = "BankIfsc",
            BankType = "BankType",
            BankBranch = "BankBranch",
            BankAdditionalInfo = "BankAdditionalInfo",
            ProjectId = "ProjectId",
            ProjectName = "ProjectName",
            EmployeeId = "EmployeeId",
            EmployeeEmpCode = "EmployeeEmpCode",
            EmployeeDepartmentId = "EmployeeDepartmentId",
            EmployeeName = "EmployeeName",
            EmployeePhone = "EmployeePhone",
            EmployeeEmail = "EmployeeEmail",
            EmployeeAddress = "EmployeeAddress",
            EmployeeProfessionalEmail = "EmployeeProfessionalEmail",
            EmployeeCityId = "EmployeeCityId",
            EmployeeStateId = "EmployeeStateId",
            EmployeePin = "EmployeePin",
            EmployeeCountry = "EmployeeCountry",
            EmployeeAdditionalInfo = "EmployeeAdditionalInfo",
            EmployeeGender = "EmployeeGender",
            EmployeeReligion = "EmployeeReligion",
            EmployeeAreaId = "EmployeeAreaId",
            EmployeeMaritalStatus = "EmployeeMaritalStatus",
            EmployeeMarriageAnniversary = "EmployeeMarriageAnniversary",
            EmployeeBirthdate = "EmployeeBirthdate",
            EmployeeDateOfJoining = "EmployeeDateOfJoining",
            EmployeeCompanyId = "EmployeeCompanyId",
            EmployeeRolesId = "EmployeeRolesId",
            EmployeeOwnerId = "EmployeeOwnerId",
            EmployeeAdhaarNo = "EmployeeAdhaarNo",
            EmployeePanNo = "EmployeePanNo",
            EmployeeAttachment = "EmployeeAttachment",
            EmployeeBankName = "EmployeeBankName",
            EmployeeAccountNumber = "EmployeeAccountNumber",
            EmployeeIfsc = "EmployeeIfsc",
            EmployeeBankType = "EmployeeBankType",
            EmployeeBranch = "EmployeeBranch",
            EmployeeTehsilId = "EmployeeTehsilId",
            EmployeeVillageId = "EmployeeVillageId",
            ProjectAmtIn = "ProjectAmtIn",
            IsCashIn = "IsCashIn",
            Purpose = "Purpose",
            RepresentativeId = "RepresentativeId",
            RepresentativeUsername = "RepresentativeUsername",
            RepresentativeDisplayName = "RepresentativeDisplayName",
            RepresentativeEmail = "RepresentativeEmail",
            RepresentativeSource = "RepresentativeSource",
            RepresentativePasswordHash = "RepresentativePasswordHash",
            RepresentativePasswordSalt = "RepresentativePasswordSalt",
            RepresentativeLastDirectoryUpdate = "RepresentativeLastDirectoryUpdate",
            RepresentativeUserImage = "RepresentativeUserImage",
            RepresentativeInsertDate = "RepresentativeInsertDate",
            RepresentativeInsertUserId = "RepresentativeInsertUserId",
            RepresentativeUpdateDate = "RepresentativeUpdateDate",
            RepresentativeUpdateUserId = "RepresentativeUpdateUserId",
            RepresentativeIsActive = "RepresentativeIsActive",
            RepresentativeUpperLevel = "RepresentativeUpperLevel",
            RepresentativeUpperLevel2 = "RepresentativeUpperLevel2",
            RepresentativeUpperLevel3 = "RepresentativeUpperLevel3",
            RepresentativeUpperLevel4 = "RepresentativeUpperLevel4",
            RepresentativeUpperLevel5 = "RepresentativeUpperLevel5",
            RepresentativeHost = "RepresentativeHost",
            RepresentativePort = "RepresentativePort",
            RepresentativeSsl = "RepresentativeSsl",
            RepresentativeEmailId = "RepresentativeEmailId",
            RepresentativeEmailPassword = "RepresentativeEmailPassword",
            RepresentativePhone = "RepresentativePhone",
            RepresentativeMcsmtpServer = "RepresentativeMcsmtpServer",
            RepresentativeMcsmtpPort = "RepresentativeMcsmtpPort",
            RepresentativeMcimapServer = "RepresentativeMcimapServer",
            RepresentativeMcimapPort = "RepresentativeMcimapPort",
            RepresentativeMcUsername = "RepresentativeMcUsername",
            RepresentativeMcPassword = "RepresentativeMcPassword",
            RepresentativeStartTime = "RepresentativeStartTime",
            RepresentativeEndTime = "RepresentativeEndTime",
            RepresentativeBranchId = "RepresentativeBranchId",
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
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
            ApprovedBy = "ApprovedBy"
        }
    }
}
