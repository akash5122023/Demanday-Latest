namespace AdvanceCRM.Settings {
    export interface WoocommerceRow {
        Id?: number;
        SiteUrl?: string;
        ConsumerKey?: string;
        ConsumerSecret?: string;
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

    export namespace WoocommerceRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SiteUrl';
        export const localTextPrefix = 'Settings.Woocommerce';
        export const deletePermission = 'Settings:Woocommerce';
        export const insertPermission = 'Settings:Woocommerce';
        export const readPermission = 'Settings:Woocommerce';
        export const updatePermission = 'Settings:Woocommerce';

        export declare const enum Fields {
            Id = "Id",
            SiteUrl = "SiteUrl",
            ConsumerKey = "ConsumerKey",
            ConsumerSecret = "ConsumerSecret",
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
