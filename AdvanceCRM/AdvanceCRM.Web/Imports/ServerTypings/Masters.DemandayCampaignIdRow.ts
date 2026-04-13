namespace AdvanceCRM.Masters {
    export interface DemandayCampaignIdRow {
        Id?: number;
        CampaignId?: string;
        DemandayMasterAccountId?: number;
        DemandayMasterAccountAccountNumber?: string;
    }

    export namespace DemandayCampaignIdRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CampaignId';
        export const localTextPrefix = 'Masters.DemandayCampaignId';
        export const lookupKey = 'Masters.DemandayCampaignId';

        export function getLookup(): Q.Lookup<DemandayCampaignIdRow> {
            return Q.getLookup<DemandayCampaignIdRow>('Masters.DemandayCampaignId');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            CampaignId = "CampaignId",
            DemandayMasterAccountId = "DemandayMasterAccountId",
            DemandayMasterAccountAccountNumber = "DemandayMasterAccountAccountNumber"
        }
    }
}
