namespace AdvanceCRM.Masters {
    export interface DemandaySubIndustryMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace DemandaySubIndustryMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.DemandaySubIndustryMaster';
        export const lookupKey = 'Masters.DemandaySubIndustryMaster';

        export function getLookup(): Q.Lookup<DemandaySubIndustryMasterRow> {
            return Q.getLookup<DemandaySubIndustryMasterRow>('Masters.DemandaySubIndustryMaster');
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
