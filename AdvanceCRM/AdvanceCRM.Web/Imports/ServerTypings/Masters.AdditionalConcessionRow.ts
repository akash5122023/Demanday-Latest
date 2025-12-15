namespace AdvanceCRM.Masters {
    export interface AdditionalConcessionRow {
        Id?: number;
        Name?: string;
        Percentage?: number;
        Amount?: number;
    }

    export namespace AdditionalConcessionRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.AdditionalConcession';
        export const lookupKey = 'Masters.AdditionalConcession';

        export function getLookup(): Q.Lookup<AdditionalConcessionRow> {
            return Q.getLookup<AdditionalConcessionRow>('Masters.AdditionalConcession');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Percentage = "Percentage",
            Amount = "Amount"
        }
    }
}
