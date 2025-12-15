namespace AdvanceCRM.Masters {
    export interface TaskTypeRow {
        Id?: number;
        Type?: string;
    }

    export namespace TaskTypeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Type';
        export const localTextPrefix = 'Masters.TaskType';
        export const lookupKey = 'Masters.TaskType';

        export function getLookup(): Q.Lookup<TaskTypeRow> {
            return Q.getLookup<TaskTypeRow>('Masters.TaskType');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Type = "Type"
        }
    }
}
