namespace AdvanceCRM.BizMail {
    export interface BmCampaignRow {
        Id?: number;
        CampaignId?: string;
        Name?: string;
        Status?: string;
    }

    export namespace BmCampaignRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CampaignId';
        export const localTextPrefix = 'BizMail.BmCampaign';
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            CampaignId = "CampaignId",
            Name = "Name",
            Status = "Status"
        }
    }
}
