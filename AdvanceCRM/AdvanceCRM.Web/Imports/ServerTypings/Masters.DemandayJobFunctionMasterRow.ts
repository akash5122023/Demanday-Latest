namespace AdvanceCRM.Masters {
    export interface DemandayJobFunctionMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace DemandayJobFunctionMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.DemandayJobFunctionMaster';
        export const lookupKey = 'Masters.DemandayJobFunctionMaster';

        export function getLookup(): Q.Lookup<DemandayJobFunctionMasterRow> {
            return Q.getLookup<DemandayJobFunctionMasterRow>('Masters.DemandayJobFunctionMaster');
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
