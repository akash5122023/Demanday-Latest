namespace AdvanceCRM.Administration {
    export interface DailyWishesLogRow {
        Id?: number;
        Date?: string;
        Log?: string;
    }

    export namespace DailyWishesLogRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Log';
        export const localTextPrefix = 'Administration.DailyWishesLog';
        export const deletePermission = 'Administration:Logs';
        export const insertPermission = 'Administration:Logs';
        export const readPermission = 'Administration:Logs';
        export const updatePermission = 'Administration:Logs';

        export declare const enum Fields {
            Id = "Id",
            Date = "Date",
            Log = "Log"
        }
    }
}
