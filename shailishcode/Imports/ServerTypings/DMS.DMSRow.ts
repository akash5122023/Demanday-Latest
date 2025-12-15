namespace AdvanceCRM.DMS {
    export interface DMSRow {
        Id?: number;
        ParentId?: number;
        Title?: string;
        Files?: string;
        OwnerId?: number;
        LastUpdatedId?: number;
        CreateDate?: string;
        UpdateDate?: string;
        AssignedId?: number;
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
        LastUpdatedUsername?: string;
        LastUpdatedDisplayName?: string;
        LastUpdatedEmail?: string;
        LastUpdatedSource?: string;
        LastUpdatedPasswordHash?: string;
        LastUpdatedPasswordSalt?: string;
        LastUpdatedLastDirectoryUpdate?: string;
        LastUpdatedUserImage?: string;
        LastUpdatedInsertDate?: string;
        LastUpdatedInsertUserId?: number;
        LastUpdatedUpdateDate?: string;
        LastUpdatedUpdateUserId?: number;
        LastUpdatedIsActive?: number;
        LastUpdatedUpperLevel?: number;
        LastUpdatedUpperLevel2?: number;
        LastUpdatedUpperLevel3?: number;
        LastUpdatedUpperLevel4?: number;
        LastUpdatedUpperLevel5?: number;
        LastUpdatedHost?: string;
        LastUpdatedPort?: number;
        LastUpdatedSsl?: boolean;
        LastUpdatedEmailId?: string;
        LastUpdatedEmailPassword?: string;
        LastUpdatedPhone?: string;
        LastUpdatedMcsmtpServer?: string;
        LastUpdatedMcsmtpPort?: number;
        LastUpdatedMcimapServer?: string;
        LastUpdatedMcimapPort?: number;
        LastUpdatedMcUsername?: string;
        LastUpdatedMcPassword?: string;
        LastUpdatedStartTime?: string;
        LastUpdatedEndTime?: string;
        LastUpdatedBranchId?: number;
        LastUpdatedUid?: string;
        LastUpdatedNonOperational?: boolean;
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
        AssignedUid?: string;
        AssignedNonOperational?: boolean;
        AssignedBranchId?: number;
        AssignedCompanyId?: number;
    }

    export namespace DMSRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Title';
        export const localTextPrefix = 'DMS.DMS';
        export const lookupKey = 'DMS.DMS';

        export function getLookup(): Q.Lookup<DMSRow> {
            return Q.getLookup<DMSRow>('DMS.DMS');
        }
        export const deletePermission = 'DMS:Modify';
        export const insertPermission = 'DMS:Modify';
        export const readPermission = 'DMS:Read';
        export const updatePermission = 'DMS:Modify';

        export declare const enum Fields {
            Id = "Id",
            ParentId = "ParentId",
            Title = "Title",
            Files = "Files",
            OwnerId = "OwnerId",
            LastUpdatedId = "LastUpdatedId",
            CreateDate = "CreateDate",
            UpdateDate = "UpdateDate",
            AssignedId = "AssignedId",
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
            LastUpdatedUsername = "LastUpdatedUsername",
            LastUpdatedDisplayName = "LastUpdatedDisplayName",
            LastUpdatedEmail = "LastUpdatedEmail",
            LastUpdatedSource = "LastUpdatedSource",
            LastUpdatedPasswordHash = "LastUpdatedPasswordHash",
            LastUpdatedPasswordSalt = "LastUpdatedPasswordSalt",
            LastUpdatedLastDirectoryUpdate = "LastUpdatedLastDirectoryUpdate",
            LastUpdatedUserImage = "LastUpdatedUserImage",
            LastUpdatedInsertDate = "LastUpdatedInsertDate",
            LastUpdatedInsertUserId = "LastUpdatedInsertUserId",
            LastUpdatedUpdateDate = "LastUpdatedUpdateDate",
            LastUpdatedUpdateUserId = "LastUpdatedUpdateUserId",
            LastUpdatedIsActive = "LastUpdatedIsActive",
            LastUpdatedUpperLevel = "LastUpdatedUpperLevel",
            LastUpdatedUpperLevel2 = "LastUpdatedUpperLevel2",
            LastUpdatedUpperLevel3 = "LastUpdatedUpperLevel3",
            LastUpdatedUpperLevel4 = "LastUpdatedUpperLevel4",
            LastUpdatedUpperLevel5 = "LastUpdatedUpperLevel5",
            LastUpdatedHost = "LastUpdatedHost",
            LastUpdatedPort = "LastUpdatedPort",
            LastUpdatedSsl = "LastUpdatedSsl",
            LastUpdatedEmailId = "LastUpdatedEmailId",
            LastUpdatedEmailPassword = "LastUpdatedEmailPassword",
            LastUpdatedPhone = "LastUpdatedPhone",
            LastUpdatedMcsmtpServer = "LastUpdatedMcsmtpServer",
            LastUpdatedMcsmtpPort = "LastUpdatedMcsmtpPort",
            LastUpdatedMcimapServer = "LastUpdatedMcimapServer",
            LastUpdatedMcimapPort = "LastUpdatedMcimapPort",
            LastUpdatedMcUsername = "LastUpdatedMcUsername",
            LastUpdatedMcPassword = "LastUpdatedMcPassword",
            LastUpdatedStartTime = "LastUpdatedStartTime",
            LastUpdatedEndTime = "LastUpdatedEndTime",
            LastUpdatedBranchId = "LastUpdatedBranchId",
            LastUpdatedUid = "LastUpdatedUid",
            LastUpdatedNonOperational = "LastUpdatedNonOperational",
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
            AssignedUid = "AssignedUid",
            AssignedNonOperational = "AssignedNonOperational",
            AssignedBranchId = "AssignedBranchId",
            AssignedCompanyId = "AssignedCompanyId"
        }
    }
}
