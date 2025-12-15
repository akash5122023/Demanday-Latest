namespace AdvanceCRM.Settings {
    export interface JustDialConfigurationRow {
        Id?: number;
        Username?: string;
        Password?: string;
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
        PostUrl?: string;
        API?: string;
        SmsTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace JustDialConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Username';
        export const localTextPrefix = 'Settings.JustDialConfiguration';
        export const deletePermission = 'Settings:JustDial';
        export const insertPermission = 'Settings:JustDial';
        export const readPermission = 'Settings:JustDial';
        export const updatePermission = 'Settings:JustDial';

        export declare const enum Fields {
            Id = "Id",
            Username = "Username",
            Password = "Password",
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
            PostUrl = "PostUrl",
            API = "API",
            SmsTemplateId = "SmsTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
