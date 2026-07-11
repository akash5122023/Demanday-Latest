namespace AdvanceCRM.TeleMarketingEmailTeam {
    export interface TeleMarketingEmailTeamRow {
        Id?: number;
        MasterAccountId?: number;
        CampaignId?: number;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        Status?: TeleMarketingEmailTeamStatus;
        OwnerId?: number;
        DemandayTeleMarketingQualiltyId?: number;
        MasterAccountNumber?: string;
        CampaignCode?: string;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
    }

    export namespace TeleMarketingEmailTeamRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'TeleMarketingEmailTeam.TeleMarketingEmailTeam';
        export const deletePermission = 'TeleMarketingEmailTeam:Delete';
        export const insertPermission = 'TeleMarketingEmailTeam:Insert';
        export const readPermission = 'TeleMarketingEmailTeam:Read';
        export const updatePermission = 'TeleMarketingEmailTeam:Update';

        export declare const enum Fields {
            Id = "Id",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email",
            Status = "Status",
            OwnerId = "OwnerId",
            DemandayTeleMarketingQualiltyId = "DemandayTeleMarketingQualiltyId",
            MasterAccountNumber = "MasterAccountNumber",
            CampaignCode = "CampaignCode",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName"
        }
    }
}
