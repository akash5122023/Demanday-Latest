namespace AdvanceCRM.Settings {
    export interface TradeIndiaConfigurationRow {
        Id?: number;
        Userid?: string;
        Profileid?: string;
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
        SmsTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace TradeIndiaConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Userid';
        export const localTextPrefix = 'Settings.TradeIndiaConfiguration';
        export const deletePermission = 'Settings:TradeIndia';
        export const insertPermission = 'Settings:TradeIndia';
        export const readPermission = 'Settings:TradeIndia';
        export const updatePermission = 'Settings:TradeIndia';

        export declare const enum Fields {
            Id = "Id",
            Userid = "Userid",
            Profileid = "Profileid",
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
            SmsTemplateId = "SmsTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
