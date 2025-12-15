namespace AdvanceCRM.Masters {
    export interface AdditionalChargesRow {
        Id?: number;
        Name?: string;
        Percentage?: number;
    }

    export namespace AdditionalChargesRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.AdditionalCharges';
        export const lookupKey = 'Masters.AdditionalCharges';

        export function getLookup(): Q.Lookup<AdditionalChargesRow> {
            return Q.getLookup<AdditionalChargesRow>('Masters.AdditionalCharges');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Percentage = "Percentage"
        }
    }
}
