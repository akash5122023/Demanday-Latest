namespace AdvanceCRM.Settings {
    export interface FacebookConfigurationRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Attachment?: string;
        SMSTemplate?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        AutoEmail?: boolean;
        AutoSms?: boolean;
        AppId?: string;
        AccessTokenKey?: string;
        TokenExpiryDate?: string;
        API?: string;
        SmsTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace FacebookConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Subject';
        export const localTextPrefix = 'Settings.FacebookConfiguration';
        export const deletePermission = 'Settings:Facebook';
        export const insertPermission = 'Settings:Facebook';
        export const readPermission = 'Settings:Facebook';
        export const updatePermission = 'Settings:Facebook';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Attachment = "Attachment",
            SMSTemplate = "SMSTemplate",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            AutoEmail = "AutoEmail",
            AutoSms = "AutoSms",
            AppId = "AppId",
            AccessTokenKey = "AccessTokenKey",
            TokenExpiryDate = "TokenExpiryDate",
            API = "API",
            SmsTemplateId = "SmsTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
