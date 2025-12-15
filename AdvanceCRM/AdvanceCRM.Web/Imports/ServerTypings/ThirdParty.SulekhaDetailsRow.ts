namespace AdvanceCRM.ThirdParty {
    export interface SulekhaDetailsRow {
        Id?: number;
        UserName?: string;
        Mobile?: string;
        Email?: string;
        City?: string;
        Localities?: string;
        Comments?: string;
        Keywords?: string;
        Feedback?: string;
        IsMoved?: boolean;
        DateTime?: string;
    }

    export namespace SulekhaDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'UserName';
        export const localTextPrefix = 'ThirdParty.SulekhaDetails';
        export const deletePermission = 'SulekhaDetails:Inbox';
        export const insertPermission = 'SulekhaDetails:Inbox';
        export const readPermission = 'SulekhaDetails:Inbox';
        export const updatePermission = 'SulekhaDetails:Inbox';

        export declare const enum Fields {
            Id = "Id",
            UserName = "UserName",
            Mobile = "Mobile",
            Email = "Email",
            City = "City",
            Localities = "Localities",
            Comments = "Comments",
            Keywords = "Keywords",
            Feedback = "Feedback",
            IsMoved = "IsMoved",
            DateTime = "DateTime"
        }
    }
}
