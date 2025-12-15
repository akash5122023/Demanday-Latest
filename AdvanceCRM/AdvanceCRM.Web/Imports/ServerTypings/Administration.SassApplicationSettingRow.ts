namespace AdvanceCRM.Administration {
    export interface SassApplicationSettingRow {
        Id?: number;
        Key?: string;
        Value?: string;
    }

    export namespace SassApplicationSettingRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Administration.SassApplicationSetting';
        export const deletePermission = '*';
        export const insertPermission = '*';
        export const readPermission = '*';
        export const updatePermission = '*';

        export declare const enum Fields {
            Id = "Id",
            Key = "Key",
            Value = "Value"
        }
    }
}
