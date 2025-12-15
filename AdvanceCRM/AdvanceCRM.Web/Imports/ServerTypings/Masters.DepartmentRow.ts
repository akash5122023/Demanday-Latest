namespace AdvanceCRM.Masters {
    export interface DepartmentRow {
        Id?: number;
        Department?: string;
    }

    export namespace DepartmentRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Department';
        export const localTextPrefix = 'Masters.Department';
        export const lookupKey = 'Masters.Department';

        export function getLookup(): Q.Lookup<DepartmentRow> {
            return Q.getLookup<DepartmentRow>('Masters.Department');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Department = "Department"
        }
    }
}
