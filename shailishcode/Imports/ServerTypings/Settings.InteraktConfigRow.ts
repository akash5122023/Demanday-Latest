namespace AdvanceCRM.Settings {
    export interface InteraktConfigRow {
        Id?: number;
        SecretKey?: string;
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
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace InteraktConfigRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SecretKey';
        export const localTextPrefix = 'Settings.InteraktConfig';
        export const deletePermission = 'Settings:Interakt';
        export const insertPermission = 'Settings:Interakt';
        export const readPermission = 'Settings:Interakt';
        export const updatePermission = 'Settings:Interakt';

        export declare const enum Fields {
            Id = "Id",
            SecretKey = "SecretKey",
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
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
