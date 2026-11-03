namespace AdvanceCRM.Masters {
    export interface ContactAccountTypeMasterRow {
        Id?: number;
        Name?: string;
    }

    export namespace ContactAccountTypeMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.ContactAccountTypeMaster';
        export const lookupKey = 'Masters.ContactAccountTypeMaster';

        export function getLookup(): Q.Lookup<ContactAccountTypeMasterRow> {
            return Q.getLookup<ContactAccountTypeMasterRow>('Masters.ContactAccountTypeMaster');
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
