namespace AdvanceCRM.Administration {
    export interface LogInOutLogRow {
        Id?: number;
        Date?: string;
        Type?: Masters.AttendanceTypeMaster;
        UserId?: number;
        UserUsername?: string;
        UserDisplayName?: string;
        UserEmail?: string;
        UserSource?: string;
        UserPasswordHash?: string;
        UserPasswordSalt?: string;
        UserLastDirectoryUpdate?: string;
        UserUserImage?: string;
        UserInsertDate?: string;
        UserInsertUserId?: number;
        UserUpdateDate?: string;
        UserUpdateUserId?: number;
        UserIsActive?: number;
        UserUpperLevel?: number;
        UserUpperLevel2?: number;
        UserUpperLevel3?: number;
        UserUpperLevel4?: number;
        UserUpperLevel5?: number;
        UserHost?: string;
        UserPort?: number;
        UserSsl?: boolean;
        UserEmailId?: string;
        UserEmailPassword?: string;
        UserPhone?: string;
        UserMcsmtpServer?: string;
        UserMcsmtpPort?: number;
        UserMcimapServer?: string;
        UserMcimapPort?: number;
        UserMcUsername?: string;
        UserMcPassword?: string;
        UserStartTime?: string;
        UserEndTime?: string;
        UserBranchId?: number;
        UserUid?: string;
        UserNonOperational?: boolean;
    }

    export namespace LogInOutLogRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Administration.LogInOutLog';
        export const deletePermission = 'Administration:Logs';
        export const insertPermission = 'Administration:Logs';
        export const readPermission = 'Administration:Logs';
        export const updatePermission = 'Administration:Logs';

        export declare const enum Fields {
            Id = "Id",
            Date = "Date",
            Type = "Type",
            UserId = "UserId",
            UserUsername = "UserUsername",
            UserDisplayName = "UserDisplayName",
            UserEmail = "UserEmail",
            UserSource = "UserSource",
            UserPasswordHash = "UserPasswordHash",
            UserPasswordSalt = "UserPasswordSalt",
            UserLastDirectoryUpdate = "UserLastDirectoryUpdate",
            UserUserImage = "UserUserImage",
            UserInsertDate = "UserInsertDate",
            UserInsertUserId = "UserInsertUserId",
            UserUpdateDate = "UserUpdateDate",
            UserUpdateUserId = "UserUpdateUserId",
            UserIsActive = "UserIsActive",
            UserUpperLevel = "UserUpperLevel",
            UserUpperLevel2 = "UserUpperLevel2",
            UserUpperLevel3 = "UserUpperLevel3",
            UserUpperLevel4 = "UserUpperLevel4",
            UserUpperLevel5 = "UserUpperLevel5",
            UserHost = "UserHost",
            UserPort = "UserPort",
            UserSsl = "UserSsl",
            UserEmailId = "UserEmailId",
            UserEmailPassword = "UserEmailPassword",
            UserPhone = "UserPhone",
            UserMcsmtpServer = "UserMcsmtpServer",
            UserMcsmtpPort = "UserMcsmtpPort",
            UserMcimapServer = "UserMcimapServer",
            UserMcimapPort = "UserMcimapPort",
            UserMcUsername = "UserMcUsername",
            UserMcPassword = "UserMcPassword",
            UserStartTime = "UserStartTime",
            UserEndTime = "UserEndTime",
            UserBranchId = "UserBranchId",
            UserUid = "UserUid",
            UserNonOperational = "UserNonOperational"
        }
    }
}
