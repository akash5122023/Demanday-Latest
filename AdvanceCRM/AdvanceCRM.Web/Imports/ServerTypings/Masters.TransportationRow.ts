namespace AdvanceCRM.Masters {
    export interface TransportationRow {
        Id?: number;
        Name?: string;
        Address?: string;
        Phone?: string;
        Email?: string;
        ContactPerson?: string;
        ContactPersonPhone?: string;
        GSTIN?: string;
    }

    export namespace TransportationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Masters.Transportation';
        export const lookupKey = 'Masters.Transportation';

        export function getLookup(): Q.Lookup<TransportationRow> {
            return Q.getLookup<TransportationRow>('Masters.Transportation');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Address = "Address",
            Phone = "Phone",
            Email = "Email",
            ContactPerson = "ContactPerson",
            ContactPersonPhone = "ContactPersonPhone",
            GSTIN = "GSTIN"
        }
    }
}
