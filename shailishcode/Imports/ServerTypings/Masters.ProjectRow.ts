namespace AdvanceCRM.Masters {
    export interface ProjectRow {
        Id?: number;
        Project?: string;
    }

    export namespace ProjectRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Project';
        export const localTextPrefix = 'Masters.Project';
        export const lookupKey = 'Masters.Project';

        export function getLookup(): Q.Lookup<ProjectRow> {
            return Q.getLookup<ProjectRow>('Masters.Project');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Project = "Project"
        }
    }
}
