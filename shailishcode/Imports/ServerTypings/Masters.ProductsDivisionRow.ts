namespace AdvanceCRM.Masters {
    export interface ProductsDivisionRow {
        Id?: number;
        ProductsDivision?: string;
    }

    export namespace ProductsDivisionRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProductsDivision';
        export const localTextPrefix = 'Masters.ProductsDivision';
        export const lookupKey = 'Masters.ProductsDivision';

        export function getLookup(): Q.Lookup<ProductsDivisionRow> {
            return Q.getLookup<ProductsDivisionRow>('Masters.ProductsDivision');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            ProductsDivision = "ProductsDivision"
        }
    }
}
