namespace AdvanceCRM.Masters {
    export interface DemandayTeleMarketingEnquiryQuestionAnswersRow {
        Id?: number;
        CampaignId?: number;
        CampaignCampaignId?: string;
        QuestionId?: number;
        AnswerText?: string;
        OwnerId?: number;
        QuestionQuestionText?: string;
        QuestionCampaignId?: number;
        QuestionOwnerId?: number;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        OwnerUpperLevel?: number;
        OwnerUpperLevel2?: number;
        OwnerUpperLevel3?: number;
        OwnerUpperLevel4?: number;
        OwnerUpperLevel5?: number;
        OwnerHost?: string;
        OwnerPort?: number;
        OwnerSsl?: boolean;
        OwnerEmailId?: string;
        OwnerEmailPassword?: string;
        OwnerPhone?: string;
        OwnerMcsmtpServer?: string;
        OwnerMcsmtpPort?: number;
        OwnerMcimapServer?: string;
        OwnerMcimapPort?: number;
        OwnerMcUsername?: string;
        OwnerMcPassword?: string;
        OwnerStartTime?: string;
        OwnerEndTime?: string;
        OwnerUid?: string;
        OwnerNonOperational?: boolean;
        OwnerBranchId?: number;
        OwnerCompanyId?: number;
        OwnerEnquiry?: boolean;
        OwnerQuotation?: boolean;
        OwnerTasks?: boolean;
        OwnerContacts?: boolean;
        OwnerPurchase?: boolean;
        OwnerSales?: boolean;
        OwnerCms?: boolean;
        OwnerLocation?: string;
        OwnerCoordinates?: string;
        OwnerTeamsId?: number;
        OwnerTenantId?: number;
        OwnerUrl?: string;
        OwnerPlan?: string;
    }

    export namespace DemandayTeleMarketingEnquiryQuestionAnswersRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AnswerText';
        export const localTextPrefix = 'Masters.DemandayTeleMarketingEnquiryQuestionAnswers';
        export const lookupKey = 'Masters.DemandayTeleMarketingEnquiryQuestionAnswers';

        export function getLookup(): Q.Lookup<DemandayTeleMarketingEnquiryQuestionAnswersRow> {
            return Q.getLookup<DemandayTeleMarketingEnquiryQuestionAnswersRow>('Masters.DemandayTeleMarketingEnquiryQuestionAnswers');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            CampaignId = "CampaignId",
            CampaignCampaignId = "CampaignCampaignId",
            QuestionId = "QuestionId",
            AnswerText = "AnswerText",
            OwnerId = "OwnerId",
            QuestionQuestionText = "QuestionQuestionText",
            QuestionCampaignId = "QuestionCampaignId",
            QuestionOwnerId = "QuestionOwnerId",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            OwnerUpperLevel = "OwnerUpperLevel",
            OwnerUpperLevel2 = "OwnerUpperLevel2",
            OwnerUpperLevel3 = "OwnerUpperLevel3",
            OwnerUpperLevel4 = "OwnerUpperLevel4",
            OwnerUpperLevel5 = "OwnerUpperLevel5",
            OwnerHost = "OwnerHost",
            OwnerPort = "OwnerPort",
            OwnerSsl = "OwnerSsl",
            OwnerEmailId = "OwnerEmailId",
            OwnerEmailPassword = "OwnerEmailPassword",
            OwnerPhone = "OwnerPhone",
            OwnerMcsmtpServer = "OwnerMcsmtpServer",
            OwnerMcsmtpPort = "OwnerMcsmtpPort",
            OwnerMcimapServer = "OwnerMcimapServer",
            OwnerMcimapPort = "OwnerMcimapPort",
            OwnerMcUsername = "OwnerMcUsername",
            OwnerMcPassword = "OwnerMcPassword",
            OwnerStartTime = "OwnerStartTime",
            OwnerEndTime = "OwnerEndTime",
            OwnerUid = "OwnerUid",
            OwnerNonOperational = "OwnerNonOperational",
            OwnerBranchId = "OwnerBranchId",
            OwnerCompanyId = "OwnerCompanyId",
            OwnerEnquiry = "OwnerEnquiry",
            OwnerQuotation = "OwnerQuotation",
            OwnerTasks = "OwnerTasks",
            OwnerContacts = "OwnerContacts",
            OwnerPurchase = "OwnerPurchase",
            OwnerSales = "OwnerSales",
            OwnerCms = "OwnerCms",
            OwnerLocation = "OwnerLocation",
            OwnerCoordinates = "OwnerCoordinates",
            OwnerTeamsId = "OwnerTeamsId",
            OwnerTenantId = "OwnerTenantId",
            OwnerUrl = "OwnerUrl",
            OwnerPlan = "OwnerPlan"
        }
    }
}
