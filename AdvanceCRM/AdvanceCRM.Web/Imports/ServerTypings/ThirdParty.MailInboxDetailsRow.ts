namespace AdvanceCRM.ThirdParty {
    export interface MailInboxDetailsRow {
        Id?: number;
        Subject?: string;
        Phone?: string;
        ToName?: string;
        To?: string;
        ToAddress?: string;
        FromName?: string;
        From?: string;
        FromAddress?: string;
        CreatedDate?: string;
        Content?: string;
        Attachment?: string;
        MessageId?: string;
        IsMoved?: boolean;
    }

    export namespace MailInboxDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Subject';
        export const localTextPrefix = 'ThirdParty.MailInboxDetails';
        export const deletePermission = 'MailInbox:Inbox';
        export const insertPermission = 'MailInbox:Inbox';
        export const readPermission = 'MailInbox:Inbox';
        export const updatePermission = 'MailInbox:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Subject = "Subject",
            Phone = "Phone",
            ToName = "ToName",
            To = "To",
            ToAddress = "ToAddress",
            FromName = "FromName",
            From = "From",
            FromAddress = "FromAddress",
            CreatedDate = "CreatedDate",
            Content = "Content",
            Attachment = "Attachment",
            MessageId = "MessageId",
            IsMoved = "IsMoved"
        }
    }
}
