namespace AdvanceCRM.Settings {
    export interface WatiConfigRow {
        Id?: number;
        Url?: string;
        Token?: string;
    }

    export namespace WatiConfigRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Url';
        export const localTextPrefix = 'Settings.WatiConfig';
        export const deletePermission = 'Settings:SMS';
        export const insertPermission = 'Settings:SMS';
        export const readPermission = 'Settings:SMS';
        export const updatePermission = 'Settings:SMS';

        export declare const enum Fields {
            Id = "Id",
            Url = "Url",
            Token = "Token"
        }
    }
}
