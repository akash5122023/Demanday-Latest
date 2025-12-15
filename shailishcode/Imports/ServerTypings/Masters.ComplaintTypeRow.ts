namespace AdvanceCRM.Masters {
    export interface ComplaintTypeRow {
        Id?: number;
        ComplaintType?: string;
    }

    export namespace ComplaintTypeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ComplaintType';
        export const localTextPrefix = 'Masters.ComplaintType';
        export const lookupKey = 'Masters.ComplaintType';

        export function getLookup(): Q.Lookup<ComplaintTypeRow> {
            return Q.getLookup<ComplaintTypeRow>('Masters.ComplaintType');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            ComplaintType = "ComplaintType"
        }
    }
}
