namespace AdvanceCRM.Settings {
    export interface IVRConfigurationRow {
        Id?: number;
        IVRNumber?: string;
        ApiKey?: string;
        Plan?: string;
        IVRType?: Masters.IVRTypeMaster;
        AppId?: string;
        AppSecret?: string;
        AutoEmail?: boolean;
        AutoSms?: boolean;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Attachment?: string;
        SmsTemplate?: string;
        TemplateId?: string;
        Host?: string;
        Port?: number;
        Ssl?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        CliNumber?: string;
        Username?: string;
        Password?: string;
        PostUrl?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
        Token_Id?: string;
        userType?: string;
        Number?: string;
        RoundRobin?: boolean;
        AutoRefresh?: boolean;
        AutoRefreshTime?: number;
        Agents?: KnowlarityAgentsRow[];
    }

    export namespace IVRConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'IVRNumber';
        export const localTextPrefix = 'Settings.IVRConfiguration';
        export const lookupKey = 'Settings.IVR';

        export function getLookup(): Q.Lookup<IVRConfigurationRow> {
            return Q.getLookup<IVRConfigurationRow>('Settings.IVR');
        }
        export const deletePermission = 'Settings:IVR';
        export const insertPermission = 'Settings:IVR';
        export const readPermission = 'Settings:IVR';
        export const updatePermission = 'Settings:IVR';

        export declare const enum Fields {
            Id = "Id",
            IVRNumber = "IVRNumber",
            ApiKey = "ApiKey",
            Plan = "Plan",
            IVRType = "IVRType",
            AppId = "AppId",
            AppSecret = "AppSecret",
            AutoEmail = "AutoEmail",
            AutoSms = "AutoSms",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Attachment = "Attachment",
            SmsTemplate = "SmsTemplate",
            TemplateId = "TemplateId",
            Host = "Host",
            Port = "Port",
            Ssl = "Ssl",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            CliNumber = "CliNumber",
            Username = "Username",
            Password = "Password",
            PostUrl = "PostUrl",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId",
            Token_Id = "Token_Id",
            userType = "userType",
            Number = "Number",
            RoundRobin = "RoundRobin",
            AutoRefresh = "AutoRefresh",
            AutoRefreshTime = "AutoRefreshTime",
            Agents = "Agents"
        }
    }
}
