namespace AdvanceCRM.Settings {
    export interface TicketWebRow {
        Id?: number;
        Username?: string;
        Password?: string;
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
    }

    export namespace TicketWebRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Username';
        export const localTextPrefix = 'Settings.TicketWeb';
        export const deletePermission = 'Settings:SMS';
        export const insertPermission = 'Settings:SMS';
        export const readPermission = 'Settings:SMS';
        export const updatePermission = 'Settings:SMS';

        export declare const enum Fields {
            Id = "Id",
            Username = "Username",
            Password = "Password",
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
            EmailPassword = "EmailPassword"
        }
    }
}
