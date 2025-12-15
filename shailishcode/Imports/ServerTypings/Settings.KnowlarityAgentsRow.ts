namespace AdvanceCRM.Settings {
    export interface KnowlarityAgentsRow {
        Id?: number;
        KnowlarityId?: number;
        Name?: string;
        Number?: string;
        KnowlarityIvrNumber?: string;
        KnowlarityApiKey?: string;
        KnowlarityPlan?: string;
        KnowlarityIvrType?: number;
        KnowlarityAppId?: string;
        KnowlarityAppSecret?: string;
    }

    export namespace KnowlarityAgentsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Settings.KnowlarityAgents';
        export const lookupKey = 'Settings.KnowlarityAgents';

        export function getLookup(): Q.Lookup<KnowlarityAgentsRow> {
            return Q.getLookup<KnowlarityAgentsRow>('Settings.KnowlarityAgents');
        }
        export const deletePermission = 'Settings:IVR';
        export const insertPermission = 'Settings:IVR';
        export const readPermission = 'Settings:IVR';
        export const updatePermission = 'Settings:IVR';

        export declare const enum Fields {
            Id = "Id",
            KnowlarityId = "KnowlarityId",
            Name = "Name",
            Number = "Number",
            KnowlarityIvrNumber = "KnowlarityIvrNumber",
            KnowlarityApiKey = "KnowlarityApiKey",
            KnowlarityPlan = "KnowlarityPlan",
            KnowlarityIvrType = "KnowlarityIvrType",
            KnowlarityAppId = "KnowlarityAppId",
            KnowlarityAppSecret = "KnowlarityAppSecret"
        }
    }
}
