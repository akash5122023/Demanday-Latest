namespace AdvanceCRM.Settings {
    export interface MailInboxRow {
        Id?: number;
        Host?: string;
        Port?: number;
        Ssl?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        AutoEmail?: boolean;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Attachment?: string;
        SHost?: string;
        SPort?: number;
        Sssl?: boolean;
        SEmailId?: string;
        SEmailPassword?: string;
    }

    export namespace MailInboxRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Host';
        export const localTextPrefix = 'Settings.MailInbox';
        export const deletePermission = 'Settings:MailInbox';
        export const insertPermission = 'Settings:MailInbox';
        export const readPermission = 'Settings:MailInbox';
        export const updatePermission = 'Settings:MailInbox';

        export declare const enum Fields {
            Id = "Id",
            Host = "Host",
            Port = "Port",
            Ssl = "Ssl",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            AutoEmail = "AutoEmail",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Attachment = "Attachment",
            SHost = "SHost",
            SPort = "SPort",
            Sssl = "Sssl",
            SEmailId = "SEmailId",
            SEmailPassword = "SEmailPassword"
        }
    }
}
