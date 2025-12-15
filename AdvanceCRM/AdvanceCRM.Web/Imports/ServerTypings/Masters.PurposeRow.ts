namespace AdvanceCRM.Masters {
    export interface PurposeRow {
        Id?: number;
        Purpose?: string;
    }

    export namespace PurposeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Purpose';
        export const localTextPrefix = 'Masters.Purpose';
        export const lookupKey = 'Masters.Purpose';

        export function getLookup(): Q.Lookup<PurposeRow> {
            return Q.getLookup<PurposeRow>('Masters.Purpose');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Purpose = "Purpose"
        }
    }
}
