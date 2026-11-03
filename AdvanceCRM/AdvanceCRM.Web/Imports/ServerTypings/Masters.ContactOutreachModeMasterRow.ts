namespace AdvanceCRM.Masters {
    export interface ContactOutreachModeMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace ContactOutreachModeMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.ContactOutreachModeMaster';
        export const lookupKey = 'Masters.ContactOutreachModeMaster';

        export function getLookup(): Q.Lookup<ContactOutreachModeMasterRow> {
            return Q.getLookup<ContactOutreachModeMasterRow>('Masters.ContactOutreachModeMaster');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name"
        }
    }
}
