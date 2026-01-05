namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingEnquiryRow {
        Id?: number;
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

    export namespace DemandayTeleMarketingEnquiryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CompanyName';
        export const localTextPrefix = 'Demanday.DemandayTeleMarketingEnquiry';
        export const lookupKey = 'Demanday.DemandayTeleMarketingEnquiry';

        export function getLookup(): Q.Lookup<DemandayTeleMarketingEnquiryRow> {
            return Q.getLookup<DemandayTeleMarketingEnquiryRow>('Demanday.DemandayTeleMarketingEnquiry');
        }
        export const deletePermission = 'DemandayTeleMarketingEnquiry:Delete';
        export const insertPermission = 'DemandayTeleMarketingEnquiry:Insert';
        export const readPermission = 'DemandayTeleMarketingEnquiry:Read';
        export const updatePermission = 'DemandayTeleMarketingEnquiry:Update';

        export declare const enum Fields {
            Id = "Id",
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
