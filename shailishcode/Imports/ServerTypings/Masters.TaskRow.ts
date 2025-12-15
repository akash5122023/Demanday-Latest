namespace AdvanceCRM.Masters {
    export interface TaskRow {
        Id?: number;
        Task?: string;
    }

    export namespace TaskRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Task';
        export const localTextPrefix = 'Masters.Task';
        export const lookupKey = 'Masters.Task';

        export function getLookup(): Q.Lookup<TaskRow> {
            return Q.getLookup<TaskRow>('Masters.Task');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Task = "Task"
        }
    }
}
