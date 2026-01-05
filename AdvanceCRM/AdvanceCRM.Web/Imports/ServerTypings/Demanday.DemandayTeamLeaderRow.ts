namespace AdvanceCRM.Demanday {
    export interface DemandayTeamLeaderRow {
        EmailFormat?: string;
        Id?: number;
        DemandayEnquiryId?: number;
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
        Industry?: string;
        Revenue?: string;
        CompanyEmployeeSize?: string;
        ProfileLink?: string;
        CompanyLink?: string;
        RevenueLink?: string;
        AddressLink?: string;
        Tenurity?: string;
        Code?: string;
        Link?: string;
        Md5?: string;
        OwnerId?: number;
        DemandayEnquiryAgentsName?: string;
        DemandayEnquiryTlName?: string;
        DemandayEnquiryCompanyName?: string;
        DemandayEnquiryFirstName?: string;
        DemandayEnquiryLastName?: string;
        DemandayEnquiryTitle?: string;
        DemandayEnquiryEmail?: string;
        DemandayEnquiryWorkPhone?: string;
        DemandayEnquiryAlternativeNumber?: string;
        DemandayEnquiryStreet?: string;
        DemandayEnquiryCity?: string;
        DemandayEnquiryState?: string;
        DemandayEnquiryZipCode?: string;
        DemandayEnquiryCountry?: string;
        DemandayEnquiryCompanyEmployeeSize?: number;
        DemandayEnquiryIndustry?: string;
        DemandayEnquiryRevenue?: number;
        DemandayEnquiryProfileLink?: string;
        DemandayEnquiryCompanyLink?: string;
        DemandayEnquiryRevenueLink?: string;
        DemandayEnquiryAdressLink?: string;
        DemandayEnquiryTenurity?: string;
        DemandayEnquiryCode?: string;
        DemandayEnquiryLink?: string;
        DemandayEnquiryMd5?: string;
        DemandayEnquiryOwnerId?: number;
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

    export namespace DemandayTeamLeaderRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'Demanday.DemandayTeamLeader';
        export const lookupKey = 'Demanday.DemandayTeamLeader';

        export function getLookup(): Q.Lookup<DemandayTeamLeaderRow> {
            return Q.getLookup<DemandayTeamLeaderRow>('Demanday.DemandayTeamLeader');
        }
        export const deletePermission = 'DemandayTeamLeader:Delete';
        export const insertPermission = 'DemandayTeamLeader:Insert';
        export const readPermission = 'DemandayTeamLeader:Read';
        export const updatePermission = 'DemandayTeamLeader:Update';

        export declare const enum Fields {
            EmailFormat = "EmailFormat",
            Id = "Id",
            DemandayEnquiryId = "DemandayEnquiryId",
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
            Industry = "Industry",
            Revenue = "Revenue",
            CompanyEmployeeSize = "CompanyEmployeeSize",
            ProfileLink = "ProfileLink",
            CompanyLink = "CompanyLink",
            RevenueLink = "RevenueLink",
            AddressLink = "AddressLink",
            Tenurity = "Tenurity",
            Code = "Code",
            Link = "Link",
            Md5 = "Md5",
            OwnerId = "OwnerId",
            DemandayEnquiryAgentsName = "DemandayEnquiryAgentsName",
            DemandayEnquiryTlName = "DemandayEnquiryTlName",
            DemandayEnquiryCompanyName = "DemandayEnquiryCompanyName",
            DemandayEnquiryFirstName = "DemandayEnquiryFirstName",
            DemandayEnquiryLastName = "DemandayEnquiryLastName",
            DemandayEnquiryTitle = "DemandayEnquiryTitle",
            DemandayEnquiryEmail = "DemandayEnquiryEmail",
            DemandayEnquiryWorkPhone = "DemandayEnquiryWorkPhone",
            DemandayEnquiryAlternativeNumber = "DemandayEnquiryAlternativeNumber",
            DemandayEnquiryStreet = "DemandayEnquiryStreet",
            DemandayEnquiryCity = "DemandayEnquiryCity",
            DemandayEnquiryState = "DemandayEnquiryState",
            DemandayEnquiryZipCode = "DemandayEnquiryZipCode",
            DemandayEnquiryCountry = "DemandayEnquiryCountry",
            DemandayEnquiryCompanyEmployeeSize = "DemandayEnquiryCompanyEmployeeSize",
            DemandayEnquiryIndustry = "DemandayEnquiryIndustry",
            DemandayEnquiryRevenue = "DemandayEnquiryRevenue",
            DemandayEnquiryProfileLink = "DemandayEnquiryProfileLink",
            DemandayEnquiryCompanyLink = "DemandayEnquiryCompanyLink",
            DemandayEnquiryRevenueLink = "DemandayEnquiryRevenueLink",
            DemandayEnquiryAdressLink = "DemandayEnquiryAdressLink",
            DemandayEnquiryTenurity = "DemandayEnquiryTenurity",
            DemandayEnquiryCode = "DemandayEnquiryCode",
            DemandayEnquiryLink = "DemandayEnquiryLink",
            DemandayEnquiryMd5 = "DemandayEnquiryMd5",
            DemandayEnquiryOwnerId = "DemandayEnquiryOwnerId",
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
