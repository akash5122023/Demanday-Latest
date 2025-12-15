namespace AdvanceCRM.Administration {
    export interface AppointmentSmsLogRow {
        Id?: number;
        Date?: string;
        Log?: string;
    }

    export namespace AppointmentSmsLogRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Log';
        export const localTextPrefix = 'Administration.AppointmentSmsLog';
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
