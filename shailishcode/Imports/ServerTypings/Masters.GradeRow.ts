namespace AdvanceCRM.Masters {
    export interface GradeRow {
        Id?: number;
        Grade?: string;
    }

    export namespace GradeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Grade';
        export const localTextPrefix = 'Masters.Grade';
        export const lookupKey = 'Masters.Grade';

        export function getLookup(): Q.Lookup<GradeRow> {
            return Q.getLookup<GradeRow>('Masters.Grade');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Grade = "Grade"
        }
    }
}
