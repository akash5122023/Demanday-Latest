namespace AdvanceCRM.ThirdParty {
    export interface VisitRow {
        Id?: number;
        CompanyName?: string;
        Name?: string;
        Address?: string;
        Email?: string;
        MobileNo?: string;
        Location?: string;
        DateNTime?: string;
        Requirements?: string;
        Purpose?: string;
        Attachments?: string;
        IsMoved?: boolean;
        CreatedBy?: number;
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
    }

    export namespace VisitRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'ThirdParty.Visit';
        export const deletePermission = 'Visit:Inbox';
        export const insertPermission = 'Visit:Inbox';
        export const readPermission = 'Visit:Inbox';
        export const updatePermission = 'Visit:Inbox';

        export declare const enum Fields {
            Id = "Id",
            CompanyName = "CompanyName",
            Name = "Name",
            Address = "Address",
            Email = "Email",
            MobileNo = "MobileNo",
            Location = "Location",
            DateNTime = "DateNTime",
            Requirements = "Requirements",
            Purpose = "Purpose",
            Attachments = "Attachments",
            IsMoved = "IsMoved",
            CreatedBy = "CreatedBy",
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
            CreatedByCompanyId = "CreatedByCompanyId"
        }
    }
}
