namespace AdvanceCRM.BizMail {
    export interface BmTemplateRow {
        Id?: number;
        Name?: string;
        Content?: string;
        InlineCss?: number;
        TemplateUid?: string;
    }

    export namespace BmTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'BizMail.BmTemplate';
        export const lookupKey = 'BizMail.BMTemplate';

        export function getLookup(): Q.Lookup<BmTemplateRow> {
            return Q.getLookup<BmTemplateRow>('BizMail.BMTemplate');
        }
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Content = "Content",
            InlineCss = "InlineCss",
            TemplateUid = "TemplateUid"
        }
    }
}
