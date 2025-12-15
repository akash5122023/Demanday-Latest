namespace AdvanceCRM.Template {
    export interface OtherTemplatesRow {
        Id?: number;
        TicketSmsTemplate?: string;
        TicketSmsTemplateId?: string;
        TaskSmsTemplate?: string;
        TaskSmsTemplateId?: string;
        FeedbackSmsTemplate?: string;
        FeedbackSmsTemplateId?: string;
        FeedbackWaTemplate?: string;
        FeedbackSwaTemplateId?: string;
        TicketWaTemplate?: string;
        TicketWaTemplateId?: string;
        TaskWaTemplate?: string;
        TaskWaTemplateId?: string;
        OtpsmsTemplate?: string;
        OtpsmsTemplateId?: string;
        OtpwaTemplate?: string;
        OtpwaTemplateId?: string;
    }

    export namespace OtherTemplatesRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TicketSmsTemplate';
        export const localTextPrefix = 'Template.OtherTemplates';
        export const deletePermission = 'Administration:General';
        export const insertPermission = 'Administration:General';
        export const readPermission = 'Administration:General';
        export const updatePermission = 'Administration:General';

        export declare const enum Fields {
            Id = "Id",
            TicketSmsTemplate = "TicketSmsTemplate",
            TicketSmsTemplateId = "TicketSmsTemplateId",
            TaskSmsTemplate = "TaskSmsTemplate",
            TaskSmsTemplateId = "TaskSmsTemplateId",
            FeedbackSmsTemplate = "FeedbackSmsTemplate",
            FeedbackSmsTemplateId = "FeedbackSmsTemplateId",
            FeedbackWaTemplate = "FeedbackWaTemplate",
            FeedbackSwaTemplateId = "FeedbackSwaTemplateId",
            TicketWaTemplate = "TicketWaTemplate",
            TicketWaTemplateId = "TicketWaTemplateId",
            TaskWaTemplate = "TaskWaTemplate",
            TaskWaTemplateId = "TaskWaTemplateId",
            OtpsmsTemplate = "OtpsmsTemplate",
            OtpsmsTemplateId = "OtpsmsTemplateId",
            OtpwaTemplate = "OtpwaTemplate",
            OtpwaTemplateId = "OtpwaTemplateId"
        }
    }
}
