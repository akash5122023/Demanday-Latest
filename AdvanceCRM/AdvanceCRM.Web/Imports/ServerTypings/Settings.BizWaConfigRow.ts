namespace AdvanceCRM.Settings {
    export interface BizWaConfigRow {
        Id?: number;
        WhatsAppNo?: string;
        PhoneNoId?: string;
        Wbaid?: string;
        Accesstoken?: string;
    }

    export namespace BizWaConfigRow {
        export const idProperty = 'Id';
        export const nameProperty = 'WhatsAppNo';
        export const localTextPrefix = 'Settings.BizWaConfig';
        export const deletePermission = 'Settings:BizWA';
        export const insertPermission = 'Settings:BizWA';
        export const readPermission = 'Settings:BizWA';
        export const updatePermission = 'Settings:BizWA';

        export declare const enum Fields {
            Id = "Id",
            WhatsAppNo = "WhatsAppNo",
            PhoneNoId = "PhoneNoId",
            Wbaid = "Wbaid",
            Accesstoken = "Accesstoken"
        }
    }
}
