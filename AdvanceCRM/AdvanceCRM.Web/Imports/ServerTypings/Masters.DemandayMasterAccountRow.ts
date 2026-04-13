namespace AdvanceCRM.Masters {
    export interface DemandayMasterAccountRow {
        Id?: number;
        AccountNumber?: string;
    }

    export namespace DemandayMasterAccountRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AccountNumber';
        export const localTextPrefix = 'Masters.DemandayMasterAccount';
        export const lookupKey = 'Masters.DemandayMasterAccount';

        export function getLookup(): Q.Lookup<DemandayMasterAccountRow> {
            return Q.getLookup<DemandayMasterAccountRow>('Masters.DemandayMasterAccount');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            AccountNumber = "AccountNumber"
        }
    }
}
