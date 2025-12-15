namespace AdvanceCRM.Masters {
    export interface ProductsUnitRow {
        Id?: number;
        ProductsUnit?: string;
    }

    export namespace ProductsUnitRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProductsUnit';
        export const localTextPrefix = 'Masters.ProductsUnit';
        export const lookupKey = 'Masters.ProductsUnit';

        export function getLookup(): Q.Lookup<ProductsUnitRow> {
            return Q.getLookup<ProductsUnitRow>('Masters.ProductsUnit');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            ProductsUnit = "ProductsUnit"
        }
    }
}
