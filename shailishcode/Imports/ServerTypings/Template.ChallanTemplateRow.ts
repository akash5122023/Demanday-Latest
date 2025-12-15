namespace AdvanceCRM.Template {
    export interface ChallanTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        CCEmails?: string;
        SMSTemplate?: string;
        CcsmSs?: string;
        TermsConditions?: string;
        TemplateId?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        Bcc?: string;
        CompanyId?: number;
        WaTemplate?: string;
        WaTemplateId?: string;
    }

    export namespace ChallanTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.ChallanTemplate';
        export const lookupKey = 'Template.ChallanTemplate';

        export function getLookup(): Q.Lookup<ChallanTemplateRow> {
            return Q.getLookup<ChallanTemplateRow>('Template.ChallanTemplate');
        }
        export const deletePermission = 'Template:Challan';
        export const insertPermission = 'Template:Challan';
        export const readPermission = 'Template:Challan';
        export const updatePermission = 'Template:Challan';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            CCEmails = "CCEmails",
            SMSTemplate = "SMSTemplate",
            CcsmSs = "CcsmSs",
            TermsConditions = "TermsConditions",
            TemplateId = "TemplateId",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            Bcc = "Bcc",
            CompanyId = "CompanyId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId"
        }
    }
}
