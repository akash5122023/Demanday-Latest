namespace AdvanceCRM.Toolkit {
    export interface DemandaySpecsRow {
        Id?: number;
        OrderId?: number;
        JobTitle?: string;
        JobLevel?: string;
        JobFunction?: string;
        Industry?: string;
        CompanyEmployeeSize?: string;
        AnnualRevenue?: string;
        Address?: string;
        City?: string;
        State?: string;
        ZipCode?: string;
        Country?: string;
        Comments?: string;
        AdditionalNotes?: string;
        MasterAccountId?: number;
        CampaignId?: number;
        OwnerId?: number;
        MasterAccountAccountNumber?: string;
        CampaignCampaignId?: string;
        CampaignDemandayMasterAccountId?: number;
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

    export namespace DemandaySpecsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'JobTitle';
        export const localTextPrefix = 'Toolkit.DemandaySpecs';
        export const lookupKey = 'DemandaySpecs.DemandaySpecs';

        export function getLookup(): Q.Lookup<DemandaySpecsRow> {
            return Q.getLookup<DemandaySpecsRow>('DemandaySpecs.DemandaySpecs');
        }
        export const deletePermission = 'DemandaySpecs:Delete';
        export const insertPermission = 'DemandaySpecs:Insert';
        export const readPermission = 'DemandaySpecs:Read';
        export const updatePermission = 'DemandaySpecs:Update';

        export declare const enum Fields {
            Id = "Id",
            OrderId = "OrderId",
            JobTitle = "JobTitle",
            JobLevel = "JobLevel",
            JobFunction = "JobFunction",
            Industry = "Industry",
            CompanyEmployeeSize = "CompanyEmployeeSize",
            AnnualRevenue = "AnnualRevenue",
            Address = "Address",
            City = "City",
            State = "State",
            ZipCode = "ZipCode",
            Country = "Country",
            Comments = "Comments",
            AdditionalNotes = "AdditionalNotes",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            OwnerId = "OwnerId",
            MasterAccountAccountNumber = "MasterAccountAccountNumber",
            CampaignCampaignId = "CampaignCampaignId",
            CampaignDemandayMasterAccountId = "CampaignDemandayMasterAccountId",
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
