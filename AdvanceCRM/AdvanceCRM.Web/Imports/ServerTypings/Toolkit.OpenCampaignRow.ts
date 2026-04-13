namespace AdvanceCRM.Toolkit {
    export interface OpenCampaignRow {
        Id?: number;
        CampaignId?: number;
        Domain?: string;
        DemandayUserId?: number;
        TimeStamp?: string;
        MasterAccountId?: number;
        OwnerId?: number;
        DemandayUserUsername?: string;
        DemandayUserDisplayName?: string;
        DemandayUserEmail?: string;
        DemandayUserUpperLevel?: number;
        DemandayUserUpperLevel2?: number;
        DemandayUserUpperLevel3?: number;
        DemandayUserUpperLevel4?: number;
        DemandayUserUpperLevel5?: number;
        DemandayUserHost?: string;
        DemandayUserPort?: number;
        DemandayUserSsl?: boolean;
        DemandayUserEmailId?: string;
        DemandayUserEmailPassword?: string;
        DemandayUserPhone?: string;
        DemandayUserMcsmtpServer?: string;
        DemandayUserMcsmtpPort?: number;
        DemandayUserMcimapServer?: string;
        DemandayUserMcimapPort?: number;
        DemandayUserMcUsername?: string;
        DemandayUserMcPassword?: string;
        DemandayUserStartTime?: string;
        DemandayUserEndTime?: string;
        DemandayUserUid?: string;
        DemandayUserNonOperational?: boolean;
        DemandayUserBranchId?: number;
        DemandayUserCompanyId?: number;
        DemandayUserEnquiry?: boolean;
        DemandayUserQuotation?: boolean;
        DemandayUserTasks?: boolean;
        DemandayUserContacts?: boolean;
        DemandayUserPurchase?: boolean;
        DemandayUserSales?: boolean;
        DemandayUserCms?: boolean;
        DemandayUserLocation?: string;
        DemandayUserCoordinates?: string;
        DemandayUserTeamsId?: number;
        DemandayUserTenantId?: number;
        DemandayUserUrl?: string;
        DemandayUserPlan?: string;
        MasterAccountAccountNumber?: string;
        CampaignIdValue?: string;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
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
        OwnerTenantId?: number;
        OwnerUrl?: string;
        OwnerPlan?: string;
    }

    export namespace OpenCampaignRow {
        export const idProperty = 'Id';
        export const nameProperty = 'MasterAccountAccountNumber';
        export const localTextPrefix = 'Toolkit.OpenCampaign';
        export const lookupKey = 'OpenCampaign.OpenCampaign';

        export function getLookup(): Q.Lookup<OpenCampaignRow> {
            return Q.getLookup<OpenCampaignRow>('OpenCampaign.OpenCampaign');
        }
        export const deletePermission = 'OpenCampaign:Delete';
        export const insertPermission = 'OpenCampaign:Insert';
        export const readPermission = 'OpenCampaign:Read';
        export const updatePermission = 'OpenCampaign:Update';

        export declare const enum Fields {
            Id = "Id",
            CampaignId = "CampaignId",
            Domain = "Domain",
            DemandayUserId = "DemandayUserId",
            TimeStamp = "TimeStamp",
            MasterAccountId = "MasterAccountId",
            OwnerId = "OwnerId",
            DemandayUserUsername = "DemandayUserUsername",
            DemandayUserDisplayName = "DemandayUserDisplayName",
            DemandayUserEmail = "DemandayUserEmail",
            DemandayUserUpperLevel = "DemandayUserUpperLevel",
            DemandayUserUpperLevel2 = "DemandayUserUpperLevel2",
            DemandayUserUpperLevel3 = "DemandayUserUpperLevel3",
            DemandayUserUpperLevel4 = "DemandayUserUpperLevel4",
            DemandayUserUpperLevel5 = "DemandayUserUpperLevel5",
            DemandayUserHost = "DemandayUserHost",
            DemandayUserPort = "DemandayUserPort",
            DemandayUserSsl = "DemandayUserSsl",
            DemandayUserEmailId = "DemandayUserEmailId",
            DemandayUserEmailPassword = "DemandayUserEmailPassword",
            DemandayUserPhone = "DemandayUserPhone",
            DemandayUserMcsmtpServer = "DemandayUserMcsmtpServer",
            DemandayUserMcsmtpPort = "DemandayUserMcsmtpPort",
            DemandayUserMcimapServer = "DemandayUserMcimapServer",
            DemandayUserMcimapPort = "DemandayUserMcimapPort",
            DemandayUserMcUsername = "DemandayUserMcUsername",
            DemandayUserMcPassword = "DemandayUserMcPassword",
            DemandayUserStartTime = "DemandayUserStartTime",
            DemandayUserEndTime = "DemandayUserEndTime",
            DemandayUserUid = "DemandayUserUid",
            DemandayUserNonOperational = "DemandayUserNonOperational",
            DemandayUserBranchId = "DemandayUserBranchId",
            DemandayUserCompanyId = "DemandayUserCompanyId",
            DemandayUserEnquiry = "DemandayUserEnquiry",
            DemandayUserQuotation = "DemandayUserQuotation",
            DemandayUserTasks = "DemandayUserTasks",
            DemandayUserContacts = "DemandayUserContacts",
            DemandayUserPurchase = "DemandayUserPurchase",
            DemandayUserSales = "DemandayUserSales",
            DemandayUserCms = "DemandayUserCms",
            DemandayUserLocation = "DemandayUserLocation",
            DemandayUserCoordinates = "DemandayUserCoordinates",
            DemandayUserTeamsId = "DemandayUserTeamsId",
            DemandayUserTenantId = "DemandayUserTenantId",
            DemandayUserUrl = "DemandayUserUrl",
            DemandayUserPlan = "DemandayUserPlan",
            MasterAccountAccountNumber = "MasterAccountAccountNumber",
            CampaignIdValue = "CampaignIdValue",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
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
            OwnerTeamsId = "OwnerTeamsId",
            OwnerTenantId = "OwnerTenantId",
            OwnerUrl = "OwnerUrl",
            OwnerPlan = "OwnerPlan"
        }
    }
}
