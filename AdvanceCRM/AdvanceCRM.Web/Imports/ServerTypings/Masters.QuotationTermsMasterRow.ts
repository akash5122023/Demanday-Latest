namespace AdvanceCRM.Masters {
    export interface QuotationTermsMasterRow {
        Id?: number;
        Terms?: string;
    }

    export namespace QuotationTermsMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Terms';
        export const localTextPrefix = 'Masters.QuotationTermsMaster';
        export const lookupKey = 'Masters.QuotationTermsMaster';

        export function getLookup(): Q.Lookup<QuotationTermsMasterRow> {
            return Q.getLookup<QuotationTermsMasterRow>('Masters.QuotationTermsMaster');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Terms = "Terms"
        }
    }
}
