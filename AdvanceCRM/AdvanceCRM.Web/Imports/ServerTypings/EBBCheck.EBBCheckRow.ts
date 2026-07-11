namespace AdvanceCRM.EBBCheck {
    export interface EBBCheckRow {
        Id?: number;
        FirstName?: string;
        Email?: string;
        Status?: EbbStatus;
        UserId?: number;
        TimeStamp?: string;
        Date?: string;
        OwnerId?: number;
        UserName?: string;
        UserDisplayName?: string;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
    }

    export namespace EBBCheckRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'EBBCheck.EBBCheck';
        export const deletePermission = 'EBBCheck:Delete';
        export const insertPermission = 'EBBCheck:Insert';
        export const readPermission = 'EBBCheck:Read';
        export const updatePermission = 'EBBCheck:Update';

        export declare const enum Fields {
            Id = "Id",
            FirstName = "FirstName",
            Email = "Email",
            Status = "Status",
            UserId = "UserId",
            TimeStamp = "TimeStamp",
            Date = "Date",
            OwnerId = "OwnerId",
            UserName = "UserName",
            UserDisplayName = "UserDisplayName",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName"
        }
    }
}
