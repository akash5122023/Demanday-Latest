namespace AdvanceCRM.BizMail {
    export interface BmSubscribersRow {
        Id?: number;
        SubscriberId?: string;
        Email?: string;
        FirstName?: string;
        LastName?: string;
        Status?: string;
        Source?: string;
        IpAddress?: string;
        DateAdded?: string;
        ListName?: string;
        ListId?: string;
        Phone?: string;
        IsMoved?: boolean;
    }

    export namespace BmSubscribersRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SubscriberId';
        export const localTextPrefix = 'BizMail.BmSubscribers';
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            SubscriberId = "SubscriberId",
            Email = "Email",
            FirstName = "FirstName",
            LastName = "LastName",
            Status = "Status",
            Source = "Source",
            IpAddress = "IpAddress",
            DateAdded = "DateAdded",
            ListName = "ListName",
            ListId = "ListId",
            Phone = "Phone",
            IsMoved = "IsMoved"
        }
    }
}
