namespace AdvanceCRM.EmailTeam {
    export interface EmailTeamRow {
        Id?: number;
        MasterAccountId?: number;
        CampaignId?: number;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        Status?: EmailTeamStatus;
        OwnerId?: number;
        DemandayQualityId?: number;
        MasterAccountNumber?: string;
        CampaignCode?: string;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
    }

    export namespace EmailTeamRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'EmailTeam.EmailTeam';
        export const deletePermission = 'EmailTeam:Delete';
        export const insertPermission = 'EmailTeam:Insert';
        export const readPermission = 'EmailTeam:Read';
        export const updatePermission = 'EmailTeam:Update';

        export declare const enum Fields {
            Id = "Id",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email",
            Status = "Status",
            OwnerId = "OwnerId",
            DemandayQualityId = "DemandayQualityId",
            MasterAccountNumber = "MasterAccountNumber",
            CampaignCode = "CampaignCode",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName"
        }
    }
}
