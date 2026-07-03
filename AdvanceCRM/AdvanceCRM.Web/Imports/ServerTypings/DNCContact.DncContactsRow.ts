namespace AdvanceCRM.DNCContact {
    export interface DncContactsRow {
        Id?: number;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        DncStatus?: string;
        Number?: string;
        CampaignId?: number;
        MasterAccountId?: number;
        CampaignCampaignId?: string;
        CampaignDemandayMasterAccountId?: number;
        MasterAccountAccountNumber?: string;
    }

    export namespace DncContactsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'DNCContact.DncContacts';
        export const lookupKey = 'DNCContact.DNCContacts';

        export function getLookup(): Q.Lookup<DncContactsRow> {
            return Q.getLookup<DncContactsRow>('DNCContact.DNCContacts');
        }
        export const deletePermission = 'DNCContacts:Delete';
        export const insertPermission = 'DNCContacts:Insert';
        export const readPermission = 'DNCContacts:Read';
        export const updatePermission = 'DNCContacts:Update';

        export declare const enum Fields {
            Id = "Id",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email",
            DncStatus = "DncStatus",
            Number = "Number",
            CampaignId = "CampaignId",
            MasterAccountId = "MasterAccountId",
            CampaignCampaignId = "CampaignCampaignId",
            CampaignDemandayMasterAccountId = "CampaignDemandayMasterAccountId",
            MasterAccountAccountNumber = "MasterAccountAccountNumber"
        }
    }
}
