namespace AdvanceCRM.Contacts {
    export interface MultiProjectsRow {
        Id?: number;
        ProjectId?: number;
        SubContactsId?: number;
        Project?: string;
        SubContactsName?: string;
        SubContactsPhone?: string;
        SubContactsResidentialPhone?: string;
        SubContactsEmail?: string;
        SubContactsDesignation?: string;
        SubContactsAddress?: string;
        SubContactsGender?: number;
        SubContactsReligion?: number;
        SubContactsMaritalStatus?: number;
        SubContactsMarriageAnniversary?: string;
        SubContactsBirthdate?: string;
        SubContactsContactsId?: number;
        SubContactsProject?: string;
        SubContactsWhatsapp?: string;
    }

    export namespace MultiProjectsRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Contacts.MultiProjects';
        export const lookupKey = 'Contacts.MultiProjects';

        export function getLookup(): Q.Lookup<MultiProjectsRow> {
            return Q.getLookup<MultiProjectsRow>('Contacts.MultiProjects');
        }
        export const deletePermission = 'Contacts:Delete';
        export const insertPermission = 'Contacts:Insert';
        export const readPermission = 'Contacts:Read';
        export const updatePermission = 'Contacts:Update';

        export declare const enum Fields {
            Id = "Id",
            ProjectId = "ProjectId",
            SubContactsId = "SubContactsId",
            Project = "Project",
            SubContactsName = "SubContactsName",
            SubContactsPhone = "SubContactsPhone",
            SubContactsResidentialPhone = "SubContactsResidentialPhone",
            SubContactsEmail = "SubContactsEmail",
            SubContactsDesignation = "SubContactsDesignation",
            SubContactsAddress = "SubContactsAddress",
            SubContactsGender = "SubContactsGender",
            SubContactsReligion = "SubContactsReligion",
            SubContactsMaritalStatus = "SubContactsMaritalStatus",
            SubContactsMarriageAnniversary = "SubContactsMarriageAnniversary",
            SubContactsBirthdate = "SubContactsBirthdate",
            SubContactsContactsId = "SubContactsContactsId",
            SubContactsProject = "SubContactsProject",
            SubContactsWhatsapp = "SubContactsWhatsapp"
        }
    }
}
