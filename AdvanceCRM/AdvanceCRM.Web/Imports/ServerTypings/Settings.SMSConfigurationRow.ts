namespace AdvanceCRM.Settings {
    export interface SMSConfigurationRow {
        Id?: number;
        Username?: string;
        Password?: string;
        SenderId?: string;
        Key?: string;
        API?: string;
        BulkAPI?: string;
        ScheduleAPI?: string;
        SuccessResponse?: string;
    }

    export namespace SMSConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Username';
        export const localTextPrefix = 'Settings.SMSConfiguration';
        export const deletePermission = 'Settings:SMS';
        export const insertPermission = 'Settings:SMS';
        export const readPermission = 'Settings:SMS';
        export const updatePermission = 'Settings:SMS';

        export declare const enum Fields {
            Id = "Id",
            Username = "Username",
            Password = "Password",
            SenderId = "SenderId",
            Key = "Key",
            API = "API",
            BulkAPI = "BulkAPI",
            ScheduleAPI = "ScheduleAPI",
            SuccessResponse = "SuccessResponse"
        }
    }
}
