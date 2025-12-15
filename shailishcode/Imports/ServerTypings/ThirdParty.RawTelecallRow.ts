namespace AdvanceCRM.ThirdParty {
    export interface RawTelecallRow {
        Id?: number;
        CompanyName?: string;
        Name?: string;
        Phone?: string;
        Email?: string;
        Details?: string;
        CreatedBy?: number;
        AssignedTo?: number;
        IsMoved?: boolean;
        Feedback?: string;
        CreatedByUsername?: string;
        CreatedByDisplayName?: string;
        CreatedByEmail?: string;
        CreatedBySource?: string;
        CreatedByPasswordHash?: string;
        CreatedByPasswordSalt?: string;
        CreatedByLastDirectoryUpdate?: string;
        CreatedByUserImage?: string;
        CreatedByInsertDate?: string;
        CreatedByInsertUserId?: number;
        CreatedByUpdateDate?: string;
        CreatedByUpdateUserId?: number;
        CreatedByIsActive?: number;
        CreatedByUpperLevel?: number;
        CreatedByUpperLevel2?: number;
        CreatedByUpperLevel3?: number;
        CreatedByUpperLevel4?: number;
        CreatedByUpperLevel5?: number;
        CreatedByHost?: string;
        CreatedByPort?: number;
        CreatedBySsl?: boolean;
        CreatedByEmailId?: string;
        CreatedByEmailPassword?: string;
        CreatedByPhone?: string;
        CreatedByMcsmtpServer?: string;
        CreatedByMcsmtpPort?: number;
        CreatedByMcimapServer?: string;
        CreatedByMcimapPort?: number;
        CreatedByMcUsername?: string;
        CreatedByMcPassword?: string;
        CreatedByStartTime?: string;
        CreatedByEndTime?: string;
        CreatedByUid?: string;
        CreatedByNonOperational?: boolean;
        CreatedByBranchId?: number;
        CreatedByCompanyId?: number;
        CreatedByEnquiry?: boolean;
        CreatedByQuotation?: boolean;
        CreatedByTasks?: boolean;
        CreatedByContacts?: boolean;
        CreatedByPurchase?: boolean;
        CreatedBySales?: boolean;
        CreatedByCms?: boolean;
        AssignedToUsername?: string;
        AssignedToDisplayName?: string;
        AssignedToEmail?: string;
        AssignedToSource?: string;
        AssignedToPasswordHash?: string;
        AssignedToPasswordSalt?: string;
        AssignedToLastDirectoryUpdate?: string;
        AssignedToUserImage?: string;
        AssignedToInsertDate?: string;
        AssignedToInsertUserId?: number;
        AssignedToUpdateDate?: string;
        AssignedToUpdateUserId?: number;
        AssignedToIsActive?: number;
        AssignedToUpperLevel?: number;
        AssignedToUpperLevel2?: number;
        AssignedToUpperLevel3?: number;
        AssignedToUpperLevel4?: number;
        AssignedToUpperLevel5?: number;
        AssignedToHost?: string;
        AssignedToPort?: number;
        AssignedToSsl?: boolean;
        AssignedToEmailId?: string;
        AssignedToEmailPassword?: string;
        AssignedToPhone?: string;
        AssignedToMcsmtpServer?: string;
        AssignedToMcsmtpPort?: number;
        AssignedToMcimapServer?: string;
        AssignedToMcimapPort?: number;
        AssignedToMcUsername?: string;
        AssignedToMcPassword?: string;
        AssignedToStartTime?: string;
        AssignedToEndTime?: string;
        AssignedToUid?: string;
        AssignedToNonOperational?: boolean;
        AssignedToBranchId?: number;
        AssignedToCompanyId?: number;
        AssignedToEnquiry?: boolean;
        AssignedToQuotation?: boolean;
        AssignedToTasks?: boolean;
        AssignedToContacts?: boolean;
        AssignedToPurchase?: boolean;
        AssignedToSales?: boolean;
        AssignedToCms?: boolean;
    }

    export namespace RawTelecallRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'ThirdParty.RawTelecall';
        export const deletePermission = 'RawTelecall:Inbox';
        export const insertPermission = 'RawTelecall:Inbox';
        export const readPermission = 'RawTelecall:Inbox';
        export const updatePermission = 'RawTelecall:Inbox';

        export declare const enum Fields {
            Id = "Id",
            CompanyName = "CompanyName",
            Name = "Name",
            Phone = "Phone",
            Email = "Email",
            Details = "Details",
            CreatedBy = "CreatedBy",
            AssignedTo = "AssignedTo",
            IsMoved = "IsMoved",
            Feedback = "Feedback",
            CreatedByUsername = "CreatedByUsername",
            CreatedByDisplayName = "CreatedByDisplayName",
            CreatedByEmail = "CreatedByEmail",
            CreatedBySource = "CreatedBySource",
            CreatedByPasswordHash = "CreatedByPasswordHash",
            CreatedByPasswordSalt = "CreatedByPasswordSalt",
            CreatedByLastDirectoryUpdate = "CreatedByLastDirectoryUpdate",
            CreatedByUserImage = "CreatedByUserImage",
            CreatedByInsertDate = "CreatedByInsertDate",
            CreatedByInsertUserId = "CreatedByInsertUserId",
            CreatedByUpdateDate = "CreatedByUpdateDate",
            CreatedByUpdateUserId = "CreatedByUpdateUserId",
            CreatedByIsActive = "CreatedByIsActive",
            CreatedByUpperLevel = "CreatedByUpperLevel",
            CreatedByUpperLevel2 = "CreatedByUpperLevel2",
            CreatedByUpperLevel3 = "CreatedByUpperLevel3",
            CreatedByUpperLevel4 = "CreatedByUpperLevel4",
            CreatedByUpperLevel5 = "CreatedByUpperLevel5",
            CreatedByHost = "CreatedByHost",
            CreatedByPort = "CreatedByPort",
            CreatedBySsl = "CreatedBySsl",
            CreatedByEmailId = "CreatedByEmailId",
            CreatedByEmailPassword = "CreatedByEmailPassword",
            CreatedByPhone = "CreatedByPhone",
            CreatedByMcsmtpServer = "CreatedByMcsmtpServer",
            CreatedByMcsmtpPort = "CreatedByMcsmtpPort",
            CreatedByMcimapServer = "CreatedByMcimapServer",
            CreatedByMcimapPort = "CreatedByMcimapPort",
            CreatedByMcUsername = "CreatedByMcUsername",
            CreatedByMcPassword = "CreatedByMcPassword",
            CreatedByStartTime = "CreatedByStartTime",
            CreatedByEndTime = "CreatedByEndTime",
            CreatedByUid = "CreatedByUid",
            CreatedByNonOperational = "CreatedByNonOperational",
            CreatedByBranchId = "CreatedByBranchId",
            CreatedByCompanyId = "CreatedByCompanyId",
            CreatedByEnquiry = "CreatedByEnquiry",
            CreatedByQuotation = "CreatedByQuotation",
            CreatedByTasks = "CreatedByTasks",
            CreatedByContacts = "CreatedByContacts",
            CreatedByPurchase = "CreatedByPurchase",
            CreatedBySales = "CreatedBySales",
            CreatedByCms = "CreatedByCms",
            AssignedToUsername = "AssignedToUsername",
            AssignedToDisplayName = "AssignedToDisplayName",
            AssignedToEmail = "AssignedToEmail",
            AssignedToSource = "AssignedToSource",
            AssignedToPasswordHash = "AssignedToPasswordHash",
            AssignedToPasswordSalt = "AssignedToPasswordSalt",
            AssignedToLastDirectoryUpdate = "AssignedToLastDirectoryUpdate",
            AssignedToUserImage = "AssignedToUserImage",
            AssignedToInsertDate = "AssignedToInsertDate",
            AssignedToInsertUserId = "AssignedToInsertUserId",
            AssignedToUpdateDate = "AssignedToUpdateDate",
            AssignedToUpdateUserId = "AssignedToUpdateUserId",
            AssignedToIsActive = "AssignedToIsActive",
            AssignedToUpperLevel = "AssignedToUpperLevel",
            AssignedToUpperLevel2 = "AssignedToUpperLevel2",
            AssignedToUpperLevel3 = "AssignedToUpperLevel3",
            AssignedToUpperLevel4 = "AssignedToUpperLevel4",
            AssignedToUpperLevel5 = "AssignedToUpperLevel5",
            AssignedToHost = "AssignedToHost",
            AssignedToPort = "AssignedToPort",
            AssignedToSsl = "AssignedToSsl",
            AssignedToEmailId = "AssignedToEmailId",
            AssignedToEmailPassword = "AssignedToEmailPassword",
            AssignedToPhone = "AssignedToPhone",
            AssignedToMcsmtpServer = "AssignedToMcsmtpServer",
            AssignedToMcsmtpPort = "AssignedToMcsmtpPort",
            AssignedToMcimapServer = "AssignedToMcimapServer",
            AssignedToMcimapPort = "AssignedToMcimapPort",
            AssignedToMcUsername = "AssignedToMcUsername",
            AssignedToMcPassword = "AssignedToMcPassword",
            AssignedToStartTime = "AssignedToStartTime",
            AssignedToEndTime = "AssignedToEndTime",
            AssignedToUid = "AssignedToUid",
            AssignedToNonOperational = "AssignedToNonOperational",
            AssignedToBranchId = "AssignedToBranchId",
            AssignedToCompanyId = "AssignedToCompanyId",
            AssignedToEnquiry = "AssignedToEnquiry",
            AssignedToQuotation = "AssignedToQuotation",
            AssignedToTasks = "AssignedToTasks",
            AssignedToContacts = "AssignedToContacts",
            AssignedToPurchase = "AssignedToPurchase",
            AssignedToSales = "AssignedToSales",
            AssignedToCms = "AssignedToCms"
        }
    }
}
