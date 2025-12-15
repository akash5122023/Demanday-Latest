namespace AdvanceCRM.Template {
    export interface EnquiryTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Attachment?: string;
        SMSTemplate?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        CompanyId?: number;
        TemplateId?: string;
        WaTemplate?: string;
        SmsReminder?: string;
        SmsrTemplateId?: string;
        WaReminder?: string;
        WarTemplateId?: string;
        WaTemplateId?: string;
    }

    export namespace EnquiryTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.EnquiryTemplate';
        export const lookupKey = 'Template.EnquiryTemplate';

        export function getLookup(): Q.Lookup<EnquiryTemplateRow> {
            return Q.getLookup<EnquiryTemplateRow>('Template.EnquiryTemplate');
        }
        export const deletePermission = 'Template:Enquiry';
        export const insertPermission = 'Template:Enquiry';
        export const readPermission = 'Template:Enquiry';
        export const updatePermission = 'Template:Enquiry';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Attachment = "Attachment",
            SMSTemplate = "SMSTemplate",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            CompanyId = "CompanyId",
            TemplateId = "TemplateId",
            WaTemplate = "WaTemplate",
            SmsReminder = "SmsReminder",
            SmsrTemplateId = "SmsrTemplateId",
            WaReminder = "WaReminder",
            WarTemplateId = "WarTemplateId",
            WaTemplateId = "WaTemplateId"
        }
    }
}
