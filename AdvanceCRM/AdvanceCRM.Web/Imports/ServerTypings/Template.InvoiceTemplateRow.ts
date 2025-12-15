namespace AdvanceCRM.Template {
    export interface InvoiceTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        SMSTemplate?: string;
        TermsConditions?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        TemplateId?: string;
        EmailId?: string;
        EmailPassword?: string;
        CCEmails?: string;
        CcsmSs?: string;
        Attachment?: string;
        BCCEmails?: string;
        CompanyId?: number;
        WaTemplate?: string;
        SmsReminder?: string;
        SmsrTemplateId?: string;
        WaReminder?: string;
        WarTemplateId?: string;
        WaTemplateId?: string;
    }

    export namespace InvoiceTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'WaTemplate';
        export const localTextPrefix = 'Template.InvoiceTemplate';
        export const lookupKey = 'Template.InvoiceTemplate';

        export function getLookup(): Q.Lookup<InvoiceTemplateRow> {
            return Q.getLookup<InvoiceTemplateRow>('Template.InvoiceTemplate');
        }
        export const deletePermission = 'Template:Invoice';
        export const insertPermission = 'Template:Invoice';
        export const readPermission = 'Template:Invoice';
        export const updatePermission = 'Template:Invoice';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            SMSTemplate = "SMSTemplate",
            TermsConditions = "TermsConditions",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            TemplateId = "TemplateId",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            CCEmails = "CCEmails",
            CcsmSs = "CcsmSs",
            Attachment = "Attachment",
            BCCEmails = "BCCEmails",
            CompanyId = "CompanyId",
            WaTemplate = "WaTemplate",
            SmsReminder = "SmsReminder",
            SmsrTemplateId = "SmsrTemplateId",
            WaReminder = "WaReminder",
            WarTemplateId = "WarTemplateId",
            WaTemplateId = "WaTemplateId"
        }
    }
}
