namespace AdvanceCRM.Masters {
    export interface ProductsGroupRow {
        Id?: number;
        ProductsGroup?: string;
    }

    export namespace ProductsGroupRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProductsGroup';
        export const localTextPrefix = 'Masters.ProductsGroup';
        export const lookupKey = 'Masters.ProductsGroup';

        export function getLookup(): Q.Lookup<ProductsGroupRow> {
            return Q.getLookup<ProductsGroupRow>('Masters.ProductsGroup');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            ProductsGroup = "ProductsGroup"
        }
    }
}
