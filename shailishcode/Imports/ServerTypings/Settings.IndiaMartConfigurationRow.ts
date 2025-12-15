namespace AdvanceCRM.Settings {
    export interface IndiaMartConfigurationRow {
        Id?: number;
        MobileNumber?: string;
        ApiKey?: string;
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
        SMobileNumber?: string;
        SapiKey?: string;
        SmsTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace IndiaMartConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'MobileNumber';
        export const localTextPrefix = 'Settings.IndiaMartConfiguration';
        export const deletePermission = 'Settings:IndiaMART';
        export const insertPermission = 'Settings:IndiaMART';
        export const readPermission = 'Settings:IndiaMART';
        export const updatePermission = 'Settings:IndiaMART';

        export declare const enum Fields {
            Id = "Id",
            MobileNumber = "MobileNumber",
            ApiKey = "ApiKey",
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
            SMobileNumber = "SMobileNumber",
            SapiKey = "SapiKey",
            SmsTemplateId = "SmsTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
