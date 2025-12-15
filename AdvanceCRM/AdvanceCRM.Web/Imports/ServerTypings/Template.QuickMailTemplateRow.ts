namespace AdvanceCRM.Template {
    export interface QuickMailTemplateRow {
        Id?: number;
        Subject?: string;
        Name?: string;
        Message?: string;
        Attachments?: string;
    }

    export namespace QuickMailTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Template.QuickMailTemplate';
        export const lookupKey = 'Template.QuickMailTemplate';

        export function getLookup(): Q.Lookup<QuickMailTemplateRow> {
            return Q.getLookup<QuickMailTemplateRow>('Template.QuickMailTemplate');
        }
        export const deletePermission = 'Template:QuickMailTemplate';
        export const insertPermission = 'Template:QuickMailTemplate';
        export const readPermission = 'Template:QuickMailTemplate';
        export const updatePermission = 'Template:QuickMailTemplate';

        export declare const enum Fields {
            Id = "Id",
            Subject = "Subject",
            Name = "Name",
            Message = "Message",
            Attachments = "Attachments"
        }
    }
}
