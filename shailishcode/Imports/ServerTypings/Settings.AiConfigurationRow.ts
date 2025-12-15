namespace AdvanceCRM.Settings {
    export interface AiConfigurationRow {
        Id?: number;
        AiKey?: string;
    }

    export namespace AiConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AiKey';
        export const localTextPrefix = 'Settings.AiConfiguration';
        export const deletePermission = 'Settings:Ai Configuration';
        export const insertPermission = 'Settings:Ai Configuration';
        export const readPermission = 'Settings:Ai Configuration';
        export const updatePermission = 'Settings:Ai Configuration';

        export declare const enum Fields {
            Id = "Id",
            AiKey = "AiKey"
        }
    }
}
