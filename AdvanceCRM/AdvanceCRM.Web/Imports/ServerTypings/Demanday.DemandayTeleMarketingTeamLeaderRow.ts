namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingTeamLeaderRow {
        Id?: number;
        TeleMarketingEnquiryId?: number;
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
        AddressLink?: string;
        Tenurity?: string;
        Code?: string;
        Link?: string;
        Md5?: string;
        OwnerId?: number;
        TeleMarketingEnquiryCompanyName?: string;
        TeleMarketingEnquiryFirstName?: string;
        TeleMarketingEnquiryLastName?: string;
        TeleMarketingEnquiryTitle?: string;
        TeleMarketingEnquiryEmail?: string;
        TeleMarketingEnquiryWorkPhone?: string;
        TeleMarketingEnquiryAlternativeNumber?: string;
        TeleMarketingEnquiryStreet?: string;
        TeleMarketingEnquiryCity?: string;
        TeleMarketingEnquiryState?: string;
        TeleMarketingEnquiryZipCode?: string;
        TeleMarketingEnquiryCountry?: string;
        TeleMarketingEnquiryCompanyEmployeeSize?: number;
        TeleMarketingEnquiryIndustry?: string;
        TeleMarketingEnquiryRevenue?: number;
        TeleMarketingEnquiryProfileLink?: string;
        TeleMarketingEnquiryCompanyLink?: string;
        TeleMarketingEnquiryRevenueLink?: string;
        TeleMarketingEnquiryAddressLink?: string;
        TeleMarketingEnquiryTenurity?: string;
        TeleMarketingEnquiryCode?: string;
        TeleMarketingEnquiryLink?: string;
        TeleMarketingEnquiryMd5?: string;
        TeleMarketingEnquiryOwnerId?: number;
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
        EmailFormat?: string;
    }

    export namespace DemandayTeleMarketingTeamLeaderRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'Demanday.DemandayTeleMarketingTeamLeader';
        export const lookupKey = 'Demanday.DemandayTeleMarketingTeamLeader';

        export function getLookup(): Q.Lookup<DemandayTeleMarketingTeamLeaderRow> {
            return Q.getLookup<DemandayTeleMarketingTeamLeaderRow>('Demanday.DemandayTeleMarketingTeamLeader');
        }
        export const deletePermission = 'DemandayTeleMarketingTeamLeader:Delete';
        export const insertPermission = 'DemandayTeleMarketingTeamLeader:Insert';
        export const readPermission = 'DemandayTeleMarketingTeamLeader:Read';
        export const updatePermission = 'DemandayTeleMarketingTeamLeader:Update';

        export declare const enum Fields {
            Id = "Id",
            TeleMarketingEnquiryId = "TeleMarketingEnquiryId",
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
            AddressLink = "AddressLink",
            Tenurity = "Tenurity",
            Code = "Code",
            Link = "Link",
            Md5 = "Md5",
            OwnerId = "OwnerId",
            TeleMarketingEnquiryCompanyName = "TeleMarketingEnquiryCompanyName",
            TeleMarketingEnquiryFirstName = "TeleMarketingEnquiryFirstName",
            TeleMarketingEnquiryLastName = "TeleMarketingEnquiryLastName",
            TeleMarketingEnquiryTitle = "TeleMarketingEnquiryTitle",
            TeleMarketingEnquiryEmail = "TeleMarketingEnquiryEmail",
            TeleMarketingEnquiryWorkPhone = "TeleMarketingEnquiryWorkPhone",
            TeleMarketingEnquiryAlternativeNumber = "TeleMarketingEnquiryAlternativeNumber",
            TeleMarketingEnquiryStreet = "TeleMarketingEnquiryStreet",
            TeleMarketingEnquiryCity = "TeleMarketingEnquiryCity",
            TeleMarketingEnquiryState = "TeleMarketingEnquiryState",
            TeleMarketingEnquiryZipCode = "TeleMarketingEnquiryZipCode",
            TeleMarketingEnquiryCountry = "TeleMarketingEnquiryCountry",
            TeleMarketingEnquiryCompanyEmployeeSize = "TeleMarketingEnquiryCompanyEmployeeSize",
            TeleMarketingEnquiryIndustry = "TeleMarketingEnquiryIndustry",
            TeleMarketingEnquiryRevenue = "TeleMarketingEnquiryRevenue",
            TeleMarketingEnquiryProfileLink = "TeleMarketingEnquiryProfileLink",
            TeleMarketingEnquiryCompanyLink = "TeleMarketingEnquiryCompanyLink",
            TeleMarketingEnquiryRevenueLink = "TeleMarketingEnquiryRevenueLink",
            TeleMarketingEnquiryAddressLink = "TeleMarketingEnquiryAddressLink",
            TeleMarketingEnquiryTenurity = "TeleMarketingEnquiryTenurity",
            TeleMarketingEnquiryCode = "TeleMarketingEnquiryCode",
            TeleMarketingEnquiryLink = "TeleMarketingEnquiryLink",
            TeleMarketingEnquiryMd5 = "TeleMarketingEnquiryMd5",
            TeleMarketingEnquiryOwnerId = "TeleMarketingEnquiryOwnerId",
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
            OwnerPlan = "OwnerPlan",
            EmailFormat = "EmailFormat"
        }
    }
}
