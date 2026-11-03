namespace AdvanceCRM.Masters {
    export interface ContactDispositionMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace ContactDispositionMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.ContactDispositionMaster';
        export const lookupKey = 'Masters.ContactDispositionMaster';

        export function getLookup(): Q.Lookup<ContactDispositionMasterRow> {
            return Q.getLookup<ContactDispositionMasterRow>('Masters.ContactDispositionMaster');
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
