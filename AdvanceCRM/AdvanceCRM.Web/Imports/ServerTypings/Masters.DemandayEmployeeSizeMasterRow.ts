namespace AdvanceCRM.Masters {
    export interface DemandayEmployeeSizeMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace DemandayEmployeeSizeMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.DemandayEmployeeSizeMaster';
        export const lookupKey = 'Masters.DemandayEmployeeSizeMaster';

        export function getLookup(): Q.Lookup<DemandayEmployeeSizeMasterRow> {
            return Q.getLookup<DemandayEmployeeSizeMasterRow>('Masters.DemandayEmployeeSizeMaster');
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
