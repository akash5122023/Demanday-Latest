namespace AdvanceCRM.Settings {
    export interface BulkMailConfigRow {
        Id?: number;
        Host?: string;
        Port?: number;
        Ssl?: boolean;
        EmailId?: string;
        EmailPassword?: string;
    }

    export namespace BulkMailConfigRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Host';
        export const localTextPrefix = 'Settings.BulkMailConfig';
        export const deletePermission = 'Settings:BulkMailConfig';
        export const insertPermission = 'Settings:BulkMailConfig';
        export const readPermission = 'Settings:BulkMailConfig';
        export const updatePermission = 'Settings:BulkMailConfig';

        export declare const enum Fields {
            Id = "Id",
            Host = "Host",
            Port = "Port",
            Ssl = "Ssl",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword"
        }
    }
}
