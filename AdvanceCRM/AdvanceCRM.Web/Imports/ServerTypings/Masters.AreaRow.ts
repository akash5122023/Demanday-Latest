namespace AdvanceCRM.Masters {
    export interface AreaRow {
        Id?: number;
        Area?: string;
    }

    export namespace AreaRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Area';
        export const localTextPrefix = 'Masters.Area';
        export const lookupKey = 'Masters.Area';

        export function getLookup(): Q.Lookup<AreaRow> {
            return Q.getLookup<AreaRow>('Masters.Area');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Area = "Area"
        }
    }
}
