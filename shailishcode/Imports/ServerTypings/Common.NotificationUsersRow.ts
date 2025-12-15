namespace AdvanceCRM.Common {
    export interface NotificationUsersRow {
        Id?: number;
        IsChecked?: boolean;
        UserId?: number;
        NotificationsId?: number;
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
        UserUid?: string;
        UserNonOperational?: boolean;
        UserBranchId?: number;
        NotificationsModule?: Masters.NotificationModules;
        NotificationsText?: string;
        NotificationsUrl?: string;
        NotificationsInsertUserId?: number;
        NotificationsInsertDate?: string;
        InsertUser?: string;
    }

    export namespace NotificationUsersRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Common.NotificationUsers';
        export const deletePermission = '';
        export const insertPermission = '';
        export const readPermission = '';
        export const updatePermission = '';

        export declare const enum Fields {
            Id = "Id",
            IsChecked = "IsChecked",
            UserId = "UserId",
            NotificationsId = "NotificationsId",
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
            UserUid = "UserUid",
            UserNonOperational = "UserNonOperational",
            UserBranchId = "UserBranchId",
            NotificationsModule = "NotificationsModule",
            NotificationsText = "NotificationsText",
            NotificationsUrl = "NotificationsUrl",
            NotificationsInsertUserId = "NotificationsInsertUserId",
            NotificationsInsertDate = "NotificationsInsertDate",
            InsertUser = "InsertUser"
        }
    }
}
