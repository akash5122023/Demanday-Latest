namespace AdvanceCRM.Common {
    export interface NotificationsRow {
        Id?: number;
        Module?: Masters.NotificationModules;
        Text?: string;
        Url?: string;
        InsertUserId?: number;
        InsertDate?: string;
        InsertUserUsername?: string;
        InsertUserDisplayName?: string;
        InsertUserEmail?: string;
        InsertUserSource?: string;
        InsertUserPasswordHash?: string;
        InsertUserPasswordSalt?: string;
        InsertUserLastDirectoryUpdate?: string;
        InsertUserUserImage?: string;
        InsertUserInsertDate?: string;
        InsertUserInsertUserId?: number;
        InsertUserUpdateDate?: string;
        InsertUserUpdateUserId?: number;
        InsertUserIsActive?: number;
        InsertUserUpperLevel?: number;
        InsertUserUpperLevel2?: number;
        InsertUserUpperLevel3?: number;
        InsertUserUpperLevel4?: number;
        InsertUserUpperLevel5?: number;
        InsertUserHost?: string;
        InsertUserPort?: number;
        InsertUserSsl?: boolean;
        InsertUserEmailId?: string;
        InsertUserEmailPassword?: string;
        InsertUserPhone?: string;
        InsertUserMcsmtpServer?: string;
        InsertUserMcsmtpPort?: number;
        InsertUserMcimapServer?: string;
        InsertUserMcimapPort?: number;
        InsertUserMcUsername?: string;
        InsertUserMcPassword?: string;
        InsertUserStartTime?: string;
        InsertUserEndTime?: string;
        InsertUserUid?: string;
        InsertUserNonOperational?: boolean;
        InsertUserBranchId?: number;
        UserList?: number[];
    }

    export namespace NotificationsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Text';
        export const localTextPrefix = 'Common.Notifications';
        export const deletePermission = '';
        export const insertPermission = '';
        export const readPermission = '';
        export const updatePermission = '';

        export declare const enum Fields {
            Id = "Id",
            Module = "Module",
            Text = "Text",
            Url = "Url",
            InsertUserId = "InsertUserId",
            InsertDate = "InsertDate",
            InsertUserUsername = "InsertUserUsername",
            InsertUserDisplayName = "InsertUserDisplayName",
            InsertUserEmail = "InsertUserEmail",
            InsertUserSource = "InsertUserSource",
            InsertUserPasswordHash = "InsertUserPasswordHash",
            InsertUserPasswordSalt = "InsertUserPasswordSalt",
            InsertUserLastDirectoryUpdate = "InsertUserLastDirectoryUpdate",
            InsertUserUserImage = "InsertUserUserImage",
            InsertUserInsertDate = "InsertUserInsertDate",
            InsertUserInsertUserId = "InsertUserInsertUserId",
            InsertUserUpdateDate = "InsertUserUpdateDate",
            InsertUserUpdateUserId = "InsertUserUpdateUserId",
            InsertUserIsActive = "InsertUserIsActive",
            InsertUserUpperLevel = "InsertUserUpperLevel",
            InsertUserUpperLevel2 = "InsertUserUpperLevel2",
            InsertUserUpperLevel3 = "InsertUserUpperLevel3",
            InsertUserUpperLevel4 = "InsertUserUpperLevel4",
            InsertUserUpperLevel5 = "InsertUserUpperLevel5",
            InsertUserHost = "InsertUserHost",
            InsertUserPort = "InsertUserPort",
            InsertUserSsl = "InsertUserSsl",
            InsertUserEmailId = "InsertUserEmailId",
            InsertUserEmailPassword = "InsertUserEmailPassword",
            InsertUserPhone = "InsertUserPhone",
            InsertUserMcsmtpServer = "InsertUserMcsmtpServer",
            InsertUserMcsmtpPort = "InsertUserMcsmtpPort",
            InsertUserMcimapServer = "InsertUserMcimapServer",
            InsertUserMcimapPort = "InsertUserMcimapPort",
            InsertUserMcUsername = "InsertUserMcUsername",
            InsertUserMcPassword = "InsertUserMcPassword",
            InsertUserStartTime = "InsertUserStartTime",
            InsertUserEndTime = "InsertUserEndTime",
            InsertUserUid = "InsertUserUid",
            InsertUserNonOperational = "InsertUserNonOperational",
            InsertUserBranchId = "InsertUserBranchId",
            UserList = "UserList"
        }
    }
}
