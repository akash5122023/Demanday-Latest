namespace AdvanceCRM.Masters {
    export interface TeamsRow {
        Id?: number;
        Team?: string;
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
        UserUid?: string;
        UserNonOperational?: boolean;
        UserBranchId?: number;
        UserCompanyId?: number;
        UserEnquiry?: boolean;
        UserQuotation?: boolean;
        UserTasks?: boolean;
        UserContacts?: boolean;
        UserPurchase?: boolean;
        UserSales?: boolean;
        UserCms?: boolean;
        UserLocation?: string;
        UserCoordinates?: string;
        UserTeamsId?: number;
    }

    export namespace TeamsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Team';
        export const localTextPrefix = 'Masters.Teams';
        export const lookupKey = 'Masters.Teams';

        export function getLookup(): Q.Lookup<TeamsRow> {
            return Q.getLookup<TeamsRow>('Masters.Teams');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Team = "Team",
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
            UserUid = "UserUid",
            UserNonOperational = "UserNonOperational",
            UserBranchId = "UserBranchId",
            UserCompanyId = "UserCompanyId",
            UserEnquiry = "UserEnquiry",
            UserQuotation = "UserQuotation",
            UserTasks = "UserTasks",
            UserContacts = "UserContacts",
            UserPurchase = "UserPurchase",
            UserSales = "UserSales",
            UserCms = "UserCms",
            UserLocation = "UserLocation",
            UserCoordinates = "UserCoordinates",
            UserTeamsId = "UserTeamsId"
        }
    }
}
