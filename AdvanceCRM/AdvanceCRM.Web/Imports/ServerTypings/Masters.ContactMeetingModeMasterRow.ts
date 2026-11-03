namespace AdvanceCRM.Masters {
    export interface ContactMeetingModeMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace ContactMeetingModeMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.ContactMeetingModeMaster';
        export const lookupKey = 'Masters.ContactMeetingModeMaster';

        export function getLookup(): Q.Lookup<ContactMeetingModeMasterRow> {
            return Q.getLookup<ContactMeetingModeMasterRow>('Masters.ContactMeetingModeMaster');
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
