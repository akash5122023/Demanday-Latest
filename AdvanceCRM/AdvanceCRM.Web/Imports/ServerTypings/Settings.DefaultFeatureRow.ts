namespace AdvanceCRM.Settings {
    export interface DefaultFeatureRow {
        Id?: number;
        Name?: string;
    }

    export namespace DefaultFeatureRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Settings.DefaultFeature';
        export const lookupKey = 'Settings.DefaultFeature';

        export function getLookup(): Q.Lookup<DefaultFeatureRow> {
            return Q.getLookup<DefaultFeatureRow>('Settings.DefaultFeature');
        }
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name"
        }
    }
}
