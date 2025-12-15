namespace AdvanceCRM.Template {
    export interface CmsTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        EmailTemplateReceipt?: string;
        ClosedEmailTemplate?: string;
        EngineerEmailTemplate?: string;
        SMSTemplate?: string;
        ClosedSMSTemplate?: string;
        EngineerSMSTemplate?: string;
        CCEmails?: string;
        CcsmSs?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        BCCEmails?: string;
        CompanyId?: number;
        SmsTemplateId?: string;
        ClosedTemplateId?: string;
        EmgineerTemplateId?: string;
        SmsReminder?: string;
        SmsrTemplateId?: string;
        WaReminder?: string;
        WarTemplateId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
        WaClosedTemplate?: string;
        WaClosedTemplateId?: string;
        WaengTemplate?: string;
        WaengTemplateId?: string;
    }

    export namespace CmsTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.CmsTemplate';
        export const lookupKey = 'Template.CmsTemplate';

        export function getLookup(): Q.Lookup<CmsTemplateRow> {
            return Q.getLookup<CmsTemplateRow>('Template.CmsTemplate');
        }
        export const deletePermission = 'Template:CMS';
        export const insertPermission = 'Template:CMS';
        export const readPermission = 'Template:CMS';
        export const updatePermission = 'Template:CMS';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            EmailTemplateReceipt = "EmailTemplateReceipt",
            ClosedEmailTemplate = "ClosedEmailTemplate",
            EngineerEmailTemplate = "EngineerEmailTemplate",
            SMSTemplate = "SMSTemplate",
            ClosedSMSTemplate = "ClosedSMSTemplate",
            EngineerSMSTemplate = "EngineerSMSTemplate",
            CCEmails = "CCEmails",
            CcsmSs = "CcsmSs",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            BCCEmails = "BCCEmails",
            CompanyId = "CompanyId",
            SmsTemplateId = "SmsTemplateId",
            ClosedTemplateId = "ClosedTemplateId",
            EmgineerTemplateId = "EmgineerTemplateId",
            SmsReminder = "SmsReminder",
            SmsrTemplateId = "SmsrTemplateId",
            WaReminder = "WaReminder",
            WarTemplateId = "WarTemplateId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId",
            WaClosedTemplate = "WaClosedTemplate",
            WaClosedTemplateId = "WaClosedTemplateId",
            WaengTemplate = "WaengTemplate",
            WaengTemplateId = "WaengTemplateId"
        }
    }
}
