namespace AdvanceCRM.Employee {
    export interface EmployeeAssestsRow {
        Id?: number;
        Items?: string;
        Quantity?: number;
        Description?: string;
        EmployeeId?: number;
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
        EmployeeEmpCode?: string;
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
    }

    export namespace EmployeeAssestsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Items';
        export const localTextPrefix = 'Employee.EmployeeAssests';
        export const deletePermission = 'Employee:Assests';
        export const insertPermission = 'Employee:Assests';
        export const readPermission = '?';
        export const updatePermission = 'Employee:Assests';

        export declare const enum Fields {
            Id = "Id",
            Items = "Items",
            Quantity = "Quantity",
            Description = "Description",
            EmployeeId = "EmployeeId",
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
            EmployeeEmpCode = "EmployeeEmpCode",
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
            OwnerTeamsId = "OwnerTeamsId"
        }
    }
}
