namespace AdvanceCRM.Masters {
    export interface CategoryRow {
        Id?: number;
        Category?: string;
        Type?: number;
    }

    export namespace CategoryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Category';
        export const localTextPrefix = 'Masters.Category';
        export const lookupKey = 'Masters.Category';

        export function getLookup(): Q.Lookup<CategoryRow> {
            return Q.getLookup<CategoryRow>('Masters.Category');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Category = "Category",
            Type = "Type"
        }
    }
}
