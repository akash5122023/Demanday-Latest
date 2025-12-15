namespace AdvanceCRM.ThirdParty {
    export interface JustDialDetailsRow {
        Id?: number;
        LeadId?: string;
        LeadType?: string;
        Prefix?: string;
        Name?: string;
        Feedback?: string;
        Mobile?: string;
        Landline?: string;
        Email?: string;
        DateTime?: string;
        Category?: string;
        City?: string;
        Area?: string;
        BranchArea?: string;
        DcnMobile?: boolean;
        DcnPhone?: boolean;
        Company?: string;
        Pin?: string;
        BranhPin?: string;
        ParentId?: string;
        IsMoved?: boolean;
    }

    export namespace JustDialDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.JustDialDetails';
        export const deletePermission = 'JustDial:Inbox';
        export const insertPermission = 'JustDial:Inbox';
        export const readPermission = 'JustDial:Inbox';
        export const updatePermission = 'JustDial:Inbox';

        export declare const enum Fields {
            Id = "Id",
            LeadId = "LeadId",
            LeadType = "LeadType",
            Prefix = "Prefix",
            Name = "Name",
            Feedback = "Feedback",
            Mobile = "Mobile",
            Landline = "Landline",
            Email = "Email",
            DateTime = "DateTime",
            Category = "Category",
            City = "City",
            Area = "Area",
            BranchArea = "BranchArea",
            DcnMobile = "DcnMobile",
            DcnPhone = "DcnPhone",
            Company = "Company",
            Pin = "Pin",
            BranhPin = "BranhPin",
            ParentId = "ParentId",
            IsMoved = "IsMoved"
        }
    }
}
