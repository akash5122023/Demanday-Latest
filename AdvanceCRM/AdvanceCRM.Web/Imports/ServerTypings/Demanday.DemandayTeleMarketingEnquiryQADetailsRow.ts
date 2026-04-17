namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingEnquiryQADetailsRow {
        Id?: number;
        EnquiryId?: number;
        QuestionId?: number;
        AnswerId?: number;
        QuestionText?: string;
        AnswerText?: string;
        CampaignId?: string;
    }

    export namespace DemandayTeleMarketingEnquiryQADetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'QuestionText';
        export const localTextPrefix = 'Demanday.DemandayTeleMarketingEnquiryQADetails';
        export const deletePermission = 'DemandayTeleMarketingEnquiry:Delete';
        export const insertPermission = 'DemandayTeleMarketingEnquiry:Insert';
        export const readPermission = 'DemandayTeleMarketingEnquiry:Read';
        export const updatePermission = 'DemandayTeleMarketingEnquiry:Update';

        export declare const enum Fields {
            Id = "Id",
            EnquiryId = "EnquiryId",
            QuestionId = "QuestionId",
            AnswerId = "AnswerId",
            QuestionText = "QuestionText",
            AnswerText = "AnswerText",
            CampaignId = "CampaignId"
        }
    }
}
