namespace AdvanceCRM.Masters {
    export interface DemandayJobLevelMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace DemandayJobLevelMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.DemandayJobLevelMaster';
        export const lookupKey = 'Masters.DemandayJobLevelMaster';

        export function getLookup(): Q.Lookup<DemandayJobLevelMasterRow> {
            return Q.getLookup<DemandayJobLevelMasterRow>('Masters.DemandayJobLevelMaster');
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
