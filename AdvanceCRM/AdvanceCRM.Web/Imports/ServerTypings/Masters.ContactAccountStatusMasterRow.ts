namespace AdvanceCRM.Masters {
    export interface ContactAccountStatusMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace ContactAccountStatusMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.ContactAccountStatusMaster';
        export const lookupKey = 'Masters.ContactAccountStatusMaster';

        export function getLookup(): Q.Lookup<ContactAccountStatusMasterRow> {
            return Q.getLookup<ContactAccountStatusMasterRow>('Masters.ContactAccountStatusMaster');
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
