namespace AdvanceCRM.ThirdParty {
    export interface InteraktUserRow {
        Id?: number;
        InteraktId?: string;
        Created?: string;
        Modified?: string;
        Phone?: string;
        CountryCode?: string;
        UserId?: string;
        FullName?: string;
        Email?: string;
        WpOptedIn?: boolean;
        IsMoved?: boolean;
    }

    export namespace InteraktUserRow {
        export const idProperty = 'Id';
        export const nameProperty = 'InteraktId';
        export const localTextPrefix = 'ThirdParty.InteraktUser';
        export const deletePermission = 'Interakt:Inbox';
        export const insertPermission = 'Interakt:Inbox';
        export const readPermission = 'Interakt:Inbox';
        export const updatePermission = 'Interakt:Inbox';

        export declare const enum Fields {
            Id = "Id",
            InteraktId = "InteraktId",
            Created = "Created",
            Modified = "Modified",
            Phone = "Phone",
            CountryCode = "CountryCode",
            UserId = "UserId",
            FullName = "FullName",
            Email = "Email",
            WpOptedIn = "WpOptedIn",
            IsMoved = "IsMoved"
        }
    }
}
