namespace AdvanceCRM.Masters {
    export interface TaxRow {
        Id?: number;
        Name?: string;
        Type?: string;
        Percentage?: number;
    }

    export namespace TaxRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.Tax';
        export const lookupKey = 'Masters.Tax';

        export function getLookup(): Q.Lookup<TaxRow> {
            return Q.getLookup<TaxRow>('Masters.Tax');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Type = "Type",
            Percentage = "Percentage"
        }
    }
}
