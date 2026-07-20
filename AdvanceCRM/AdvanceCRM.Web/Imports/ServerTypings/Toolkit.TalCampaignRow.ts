namespace AdvanceCRM.Toolkit {
    export interface TalCampaignRow {
        Id?: number;
        SrNo?: number;
        CompanyName?: string;
        Domain?: string;
        AgentsName?: number;
        Reason?: string;
        MasterAccountId?: number;
        CampaignId?: number;
        OwnerId?: number;
        Cpc?: string;
        MasterAccountAccountNumber?: string;
        CampaignIdValue?: string;
        AgentDisplayName?: string;
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

    export namespace TalCampaignRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'Toolkit.TalCampaign';
        export const lookupKey = 'TalCampaign.TalCampaign';

        export function getLookup(): Q.Lookup<TalCampaignRow> {
            return Q.getLookup<TalCampaignRow>('TalCampaign.TalCampaign');
        }
        export const deletePermission = 'TalCampaign:Delete';
        export const insertPermission = 'TalCampaign:Insert';
        export const readPermission = 'TalCampaign:Read';
        export const updatePermission = 'TalCampaign:Update';

        export declare const enum Fields {
            Id = "Id",
            SrNo = "SrNo",
            CompanyName = "CompanyName",
            Domain = "Domain",
            AgentsName = "AgentsName",
            Reason = "Reason",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            OwnerId = "OwnerId",
            Cpc = "Cpc",
            MasterAccountAccountNumber = "MasterAccountAccountNumber",
            CampaignIdValue = "CampaignIdValue",
            AgentDisplayName = "AgentDisplayName",
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
