namespace AdvanceCRM.Administration {
    export interface UserRow {
        UserId?: number;
        NonOperational?: boolean;
        UID?: string;
        Username?: string;
        Source?: string;
        PasswordHash?: string;
        PasswordSalt?: string;
        DisplayName?: string;
        Email?: string;
        Phone?: string;
        BranchId?: number;
        Branch?: string;
        UserImage?: string;
        LastDirectoryUpdate?: string;
        IsActive?: boolean;
        TeamsId?: number;
        TenantId?: number;
        Url?: string;
        Plan?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        Password?: string;
        PasswordConfirm?: string;
        UpperLevel?: number;
        UpperLevelName?: string;
        UpperLevel2?: number;
        UpperLevelName2?: string;
        UpperLevel3?: number;
        UpperLevelName3?: string;
        UpperLevel4?: number;
        UpperLevelName4?: string;
        UpperLevel5?: number;
        UpperLevelName5?: string;
        MCSMTPServer?: string;
        MCSMTPPort?: number;
        MCIMAPServer?: string;
        MCIMAPPort?: number;
        MCUsername?: string;
        MCPassword?: string;
        StartTime?: string;
        EndTime?: string;
        CompanyId?: number;
        Location?: string;
        Coordinates?: string;
        Enquiry?: boolean;
        Quotation?: boolean;
        Tasks?: boolean;
        Contacts?: boolean;
        Purchase?: boolean;
        Sales?: boolean;
        Cms?: boolean;
        TeamsTeam?: string;
        TeamsUserId?: number;
        TenantName?: string;
        InsertUserId?: number;
        InsertDate?: string;
        UpdateUserId?: number;
        UpdateDate?: string;
    }

    export namespace UserRow {
        export const idProperty = 'UserId';
        export const nameProperty = 'DisplayName';
        export const localTextPrefix = 'Administration.User';
        export const lookupKey = 'Administration.User';

        export function getLookup(): Q.Lookup<UserRow> {
            return Q.getLookup<UserRow>('Administration.User');
        }
        export const deletePermission = 'Administration:Security';
        export const insertPermission = 'Administration:Security';
        export const readPermission = 'Administration:Security';
        export const updatePermission = 'Administration:Security';

        export declare const enum Fields {
            UserId = "UserId",
            NonOperational = "NonOperational",
            UID = "UID",
            Username = "Username",
            Source = "Source",
            PasswordHash = "PasswordHash",
            PasswordSalt = "PasswordSalt",
            DisplayName = "DisplayName",
            Email = "Email",
            Phone = "Phone",
            BranchId = "BranchId",
            Branch = "Branch",
            UserImage = "UserImage",
            LastDirectoryUpdate = "LastDirectoryUpdate",
            IsActive = "IsActive",
            TeamsId = "TeamsId",
            TenantId = "TenantId",
            Url = "Url",
            Plan = "Plan",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            Password = "Password",
            PasswordConfirm = "PasswordConfirm",
            UpperLevel = "UpperLevel",
            UpperLevelName = "UpperLevelName",
            UpperLevel2 = "UpperLevel2",
            UpperLevelName2 = "UpperLevelName2",
            UpperLevel3 = "UpperLevel3",
            UpperLevelName3 = "UpperLevelName3",
            UpperLevel4 = "UpperLevel4",
            UpperLevelName4 = "UpperLevelName4",
            UpperLevel5 = "UpperLevel5",
            UpperLevelName5 = "UpperLevelName5",
            MCSMTPServer = "MCSMTPServer",
            MCSMTPPort = "MCSMTPPort",
            MCIMAPServer = "MCIMAPServer",
            MCIMAPPort = "MCIMAPPort",
            MCUsername = "MCUsername",
            MCPassword = "MCPassword",
            StartTime = "StartTime",
            EndTime = "EndTime",
            CompanyId = "CompanyId",
            Location = "Location",
            Coordinates = "Coordinates",
            Enquiry = "Enquiry",
            Quotation = "Quotation",
            Tasks = "Tasks",
            Contacts = "Contacts",
            Purchase = "Purchase",
            Sales = "Sales",
            Cms = "Cms",
            TeamsTeam = "TeamsTeam",
            TeamsUserId = "TeamsUserId",
            TenantName = "TenantName",
            InsertUserId = "InsertUserId",
            InsertDate = "InsertDate",
            UpdateUserId = "UpdateUserId",
            UpdateDate = "UpdateDate"
        }
    }
}
