namespace AdvanceCRM.Administration {
    export interface OptLogRow {
        Id?: number;
        Phone?: string;
        Opt?: number;
        Validity?: string;
    }

    export namespace OptLogRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Phone';
        export const localTextPrefix = 'Administration.OptLog';
        export const deletePermission = 'Administration:Logs';
        export const insertPermission = 'Administration:Logs';
        export const readPermission = 'Administration:Logs';
        export const updatePermission = 'Administration:Logs';

        export declare const enum Fields {
            Id = "Id",
            Phone = "Phone",
            Opt = "Opt",
            Validity = "Validity"
        }
    }
}
