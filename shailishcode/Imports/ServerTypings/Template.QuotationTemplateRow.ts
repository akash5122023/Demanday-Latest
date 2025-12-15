namespace AdvanceCRM.Template {
    export interface QuotationTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        SMSTemplate?: string;
        Host?: string;
        Port?: number;
        TemplateId?: string;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        Attachment?: string;
        CCEmails?: string;
        CcsmSs?: string;
        BCCEmails?: string;
        WaTemplate?: string;
        CompanyId?: number;
        SmsReminder?: string;
        SmsrTemplateId?: string;
        WaReminder?: string;
        WarTemplateId?: string;
        WaTemplateId?: string;
    }

    export namespace QuotationTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.QuotationTemplate';
        export const lookupKey = 'Template.QuotationTemplate';

        export function getLookup(): Q.Lookup<QuotationTemplateRow> {
            return Q.getLookup<QuotationTemplateRow>('Template.QuotationTemplate');
        }
        export const deletePermission = 'Template:Quotation';
        export const insertPermission = 'Template:Quotation';
        export const readPermission = 'Template:Quotation';
        export const updatePermission = 'Template:Quotation';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            SMSTemplate = "SMSTemplate",
            Host = "Host",
            Port = "Port",
            TemplateId = "TemplateId",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            Attachment = "Attachment",
            CCEmails = "CCEmails",
            CcsmSs = "CcsmSs",
            BCCEmails = "BCCEmails",
            WaTemplate = "WaTemplate",
            CompanyId = "CompanyId",
            SmsReminder = "SmsReminder",
            SmsrTemplateId = "SmsrTemplateId",
            WaReminder = "WaReminder",
            WarTemplateId = "WarTemplateId",
            WaTemplateId = "WaTemplateId"
        }
    }
}
