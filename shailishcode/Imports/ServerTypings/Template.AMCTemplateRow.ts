namespace AdvanceCRM.Template {
    export interface AMCTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplateReceipt?: string;
        SMSTemplate?: string;
        CCEmails?: string;
        TermsConditions?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        BCCEmails?: string;
        VisitSMSTemplate?: string;
        CompanyId?: number;
        SmsTempId?: string;
        VisitTempId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
        WaVisitTemplate?: string;
        WaVisitTemplateId?: string;
    }

    export namespace AMCTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.AMCTemplate';
        export const lookupKey = 'Template.AMCTemplate';

        export function getLookup(): Q.Lookup<AMCTemplateRow> {
            return Q.getLookup<AMCTemplateRow>('Template.AMCTemplate');
        }
        export const deletePermission = 'Template:AMC';
        export const insertPermission = 'Template:AMC';
        export const readPermission = 'Template:AMC';
        export const updatePermission = 'Template:AMC';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplateReceipt = "EmailTemplateReceipt",
            SMSTemplate = "SMSTemplate",
            CCEmails = "CCEmails",
            TermsConditions = "TermsConditions",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            BCCEmails = "BCCEmails",
            VisitSMSTemplate = "VisitSMSTemplate",
            CompanyId = "CompanyId",
            SmsTempId = "SmsTempId",
            VisitTempId = "VisitTempId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId",
            WaVisitTemplate = "WaVisitTemplate",
            WaVisitTemplateId = "WaVisitTemplateId"
        }
    }
}
