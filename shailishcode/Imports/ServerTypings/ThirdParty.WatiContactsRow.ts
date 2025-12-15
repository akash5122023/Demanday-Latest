namespace AdvanceCRM.ThirdParty {
    export interface WatiContactsRow {
        Id?: number;
        Waid?: string;
        FirtName?: string;
        FullName?: string;
        Phone?: string;
        Source?: string;
        Status?: string;
        Created?: string;
        IsMoved?: boolean;
    }

    export namespace WatiContactsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Waid';
        export const localTextPrefix = 'ThirdParty.WatiContacts';
        export const deletePermission = 'WatiContacts:Inbox';
        export const insertPermission = 'WatiContacts:Inbox';
        export const readPermission = 'WatiContacts:Inbox';
        export const updatePermission = 'WatiContacts:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Waid = "Waid",
            FirtName = "FirtName",
            FullName = "FullName",
            Phone = "Phone",
            Source = "Source",
            Status = "Status",
            Created = "Created",
            IsMoved = "IsMoved"
        }
    }
}
