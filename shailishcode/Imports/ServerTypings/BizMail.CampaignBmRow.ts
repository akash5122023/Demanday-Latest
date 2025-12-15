namespace AdvanceCRM.BizMail {
    export interface CampaignBmRow {
        Id?: number;
        Campaignuid?: string;
        Name?: string;
        Type?: Masters.CampaignTypeMaster;
        FromName?: string;
        FromEmail?: string;
        Subject?: string;
        ReplyTo?: string;
        BmListId?: number;
        SendAt?: string;
        BmTemplateId?: number;
        InlineCss?: Masters.InlinecssMaster;
        AutoPlaneTest?: Masters.InlinecssMaster;
        ListId?: string;
        BmListListId?: string;
        BmListCompanyName?: string;
        BmListName?: string;
        BmListCity?: string;
        BmListDisplayName?: string;
        BmListDescription?: string;
        BmListFrom?: string;
        BmListReplyTo?: string;
        BmTemplateName?: string;
        BmTemplateContent?: string;
        BmTemplateInlineCss?: number;
    }

    export namespace CampaignBmRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Campaignuid';
        export const localTextPrefix = 'BizMail.CampaignBm';
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            Campaignuid = "Campaignuid",
            Name = "Name",
            Type = "Type",
            FromName = "FromName",
            FromEmail = "FromEmail",
            Subject = "Subject",
            ReplyTo = "ReplyTo",
            BmListId = "BmListId",
            SendAt = "SendAt",
            BmTemplateId = "BmTemplateId",
            InlineCss = "InlineCss",
            AutoPlaneTest = "AutoPlaneTest",
            ListId = "ListId",
            BmListListId = "BmListListId",
            BmListCompanyName = "BmListCompanyName",
            BmListName = "BmListName",
            BmListCity = "BmListCity",
            BmListDisplayName = "BmListDisplayName",
            BmListDescription = "BmListDescription",
            BmListFrom = "BmListFrom",
            BmListReplyTo = "BmListReplyTo",
            BmTemplateName = "BmTemplateName",
            BmTemplateContent = "BmTemplateContent",
            BmTemplateInlineCss = "BmTemplateInlineCss"
        }
    }
}
