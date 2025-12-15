namespace AdvanceCRM.Settings {
    export interface WebsiteEnquiryConfigurationRow {
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
        SmsTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
        API?: string;
    }

    export namespace WebsiteEnquiryConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Username';
        export const localTextPrefix = 'Settings.WebsiteEnquiryConfiguration';
        export const deletePermission = 'Settings:WebsiteEnquiry';
        export const insertPermission = 'Settings:WebsiteEnquiry';
        export const readPermission = 'Settings:WebsiteEnquiry';
        export const updatePermission = 'Settings:WebsiteEnquiry';

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
            SmsTemplateId = "SmsTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId",
            API = "API"
        }
    }
}
