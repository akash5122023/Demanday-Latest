namespace AdvanceCRM.Masters {
    export interface TaskStatusRow {
        Id?: number;
        Status?: string;
    }

    export namespace TaskStatusRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Status';
        export const localTextPrefix = 'Masters.TaskStatus';
        export const lookupKey = 'Masters.TaskStatus';

        export function getLookup(): Q.Lookup<TaskStatusRow> {
            return Q.getLookup<TaskStatusRow>('Masters.TaskStatus');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Status = "Status"
        }
    }
}
