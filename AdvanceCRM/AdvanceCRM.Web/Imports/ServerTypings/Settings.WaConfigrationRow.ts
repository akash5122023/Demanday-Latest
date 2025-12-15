namespace AdvanceCRM.Settings {
    export interface WaConfigrationRow {
        Id?: number;
        Mobile?: string;
        ApiKey?: string;
        MessageApi?: string;
        MediaApi?: string;
        SuccessResponse?: string;
    }

    export namespace WaConfigrationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Mobile';
        export const localTextPrefix = 'Settings.WaConfigration';
        export const deletePermission = 'Settings:WA';
        export const insertPermission = 'Settings:WA';
        export const readPermission = 'Settings:WA';
        export const updatePermission = 'Settings:WA';

        export declare const enum Fields {
            Id = "Id",
            Mobile = "Mobile",
            ApiKey = "ApiKey",
            MessageApi = "MessageApi",
            MediaApi = "MediaApi",
            SuccessResponse = "SuccessResponse"
        }
    }
}
