namespace AdvanceCRM.Template {
    export interface PurchaseOrderTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        CCEmails?: string;
        Bcc?: string;
        CompanyId?: number;
    }

    export namespace PurchaseOrderTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.PurchaseOrderTemplate';
        export const lookupKey = 'Template.PurchaseOrderTemplateRow';

        export function getLookup(): Q.Lookup<PurchaseOrderTemplateRow> {
            return Q.getLookup<PurchaseOrderTemplateRow>('Template.PurchaseOrderTemplateRow');
        }
        export const deletePermission = 'Template:PurchaseOrder';
        export const insertPermission = 'Template:PurchaseOrder';
        export const readPermission = 'Template:PurchaseOrder';
        export const updatePermission = 'Template:PurchaseOrder';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            CCEmails = "CCEmails",
            Bcc = "Bcc",
            CompanyId = "CompanyId"
        }
    }
}
