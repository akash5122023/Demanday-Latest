namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingContactsRow {
        Id?: number;
        AgentsName?: string;
        TlName?: string;
        CampaignId?: string;
        CompanyName?: string;
        FirstName?: string;
        LastName?: string;
        Title?: string;
        Email?: string;
        WorkPhone?: string;
        AlternativeNumber?: string;
        Street?: string;
        City?: string;
        State?: string;
        ZipCode?: string;
        Country?: string;
        CompanyEmployeeSize?: string;
        Industry?: string;
        Revenue?: string;
        ProfileLink?: string;
        CompanyLink?: string;
        RevenueLink?: string;
        EmailFormat?: string;
        AdressLink?: string;
        Tenurity?: string;
        Code?: string;
        Link?: string;
        Md5?: string;
        Slot?: string;
        PrimaryReason?: string;
        Category?: string;
        Comments?: string;
        QaStatus?: string;
        DeliveryStatus?: string;
        AgentName?: string;
        QaName?: string;
        CallDate?: string;
        DateAudited?: string;
        DeliveryDate?: string;
        Source?: string;
        VerificationMode?: string;
        Asset1?: string;
        Asset2?: string;
        Domain?: string;
        JobLevel?: string;
        JobFunctionRole?: string;
        Continents?: string;
        ProspectUrl?: string;
        OwnerId?: number;
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

    export namespace DemandayTeleMarketingContactsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AgentsName';
        export const localTextPrefix = 'Demanday.DemandayTeleMarketingContacts';
        export const lookupKey = 'Demanday.DemandayTeleMarketingContacts';

        export function getLookup(): Q.Lookup<DemandayTeleMarketingContactsRow> {
            return Q.getLookup<DemandayTeleMarketingContactsRow>('Demanday.DemandayTeleMarketingContacts');
        }
        export const deletePermission = 'DemandayTeleMarketingContacts:Delete';
        export const insertPermission = 'DemandayTeleMarketingContacts:Insert';
        export const readPermission = 'DemandayTeleMarketingContacts:Read';
        export const updatePermission = 'DemandayTeleMarketingContacts:Update';

        export declare const enum Fields {
            Id = "Id",
            AgentsName = "AgentsName",
            TlName = "TlName",
            CampaignId = "CampaignId",
            CompanyName = "CompanyName",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Email = "Email",
            WorkPhone = "WorkPhone",
            AlternativeNumber = "AlternativeNumber",
            Street = "Street",
            City = "City",
            State = "State",
            ZipCode = "ZipCode",
            Country = "Country",
            CompanyEmployeeSize = "CompanyEmployeeSize",
            Industry = "Industry",
            Revenue = "Revenue",
            ProfileLink = "ProfileLink",
            CompanyLink = "CompanyLink",
            RevenueLink = "RevenueLink",
            EmailFormat = "EmailFormat",
            AdressLink = "AdressLink",
            Tenurity = "Tenurity",
            Code = "Code",
            Link = "Link",
            Md5 = "Md5",
            Slot = "Slot",
            PrimaryReason = "PrimaryReason",
            Category = "Category",
            Comments = "Comments",
            QaStatus = "QaStatus",
            DeliveryStatus = "DeliveryStatus",
            AgentName = "AgentName",
            QaName = "QaName",
            CallDate = "CallDate",
            DateAudited = "DateAudited",
            DeliveryDate = "DeliveryDate",
            Source = "Source",
            VerificationMode = "VerificationMode",
            Asset1 = "Asset1",
            Asset2 = "Asset2",
            Domain = "Domain",
            JobLevel = "JobLevel",
            JobFunctionRole = "JobFunctionRole",
            Continents = "Continents",
            ProspectUrl = "ProspectUrl",
            OwnerId = "OwnerId",
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
