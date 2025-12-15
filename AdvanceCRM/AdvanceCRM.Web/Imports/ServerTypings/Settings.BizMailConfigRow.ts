namespace AdvanceCRM.Settings {
    export interface BizMailConfigRow {
        Id?: number;
        Apiurl?: string;
        Apikey?: string;
        FromName?: string;
        FromMail?: string;
        ReplyToName?: string;
        ReplyToMail?: string;
    }

    export namespace BizMailConfigRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Apiurl';
        export const localTextPrefix = 'Settings.BizMailConfig';
        export const deletePermission = 'Settings:BizMail';
        export const insertPermission = 'Settings:BizMail';
        export const readPermission = 'Settings:BizMail';
        export const updatePermission = 'Settings:BizMail';

        export declare const enum Fields {
            Id = "Id",
            Apiurl = "Apiurl",
            Apikey = "Apikey",
            FromName = "FromName",
            FromMail = "FromMail",
            ReplyToName = "ReplyToName",
            ReplyToMail = "ReplyToMail"
        }
    }
}
