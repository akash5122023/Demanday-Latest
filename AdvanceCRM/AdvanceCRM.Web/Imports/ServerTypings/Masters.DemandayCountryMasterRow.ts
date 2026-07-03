namespace AdvanceCRM.Masters {
    export interface DemandayCountryMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace DemandayCountryMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.DemandayCountryMaster';
        export const lookupKey = 'Masters.DemandayCountryMaster';

        export function getLookup(): Q.Lookup<DemandayCountryMasterRow> {
            return Q.getLookup<DemandayCountryMasterRow>('Masters.DemandayCountryMaster');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name"
        }
    }
}
