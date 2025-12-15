namespace AdvanceCRM.Masters {
    export interface DaysRow {
        Id?: number;
        Title?: string;
        Heading?: string;
        Description?: string;
        FileAttachments?: string;
    }

    export namespace DaysRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Title';
        export const localTextPrefix = 'Masters.Days';
        export const lookupKey = 'Masters.Days';

        export function getLookup(): Q.Lookup<DaysRow> {
            return Q.getLookup<DaysRow>('Masters.Days');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Title = "Title",
            Heading = "Heading",
            Description = "Description",
            FileAttachments = "FileAttachments"
        }
    }
}
