namespace AdvanceCRM.Template {
    export interface IntractTemplateRow {
        Id?: number;
        CreatedAtUtc?: string;
        Name?: string;
        IntractId?: string;
        Language?: string;
        Category?: string;
        TemplateCategoryLabel?: string;
        HeaderFormat?: string;
        Header?: string;
        Body?: string;
        Footer?: string;
        Buttons?: string;
        AutosubmittedFor?: string;
        DisplayName?: string;
        ApprovalStatus?: string;
        WaTemplateId?: string;
        VariablePresent?: string;
        header_handle_file_url?: string;
    }

    export namespace IntractTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Template.IntractTemplate';
        export const lookupKey = 'Template.IntractTemplate';

        export function getLookup(): Q.Lookup<IntractTemplateRow> {
            return Q.getLookup<IntractTemplateRow>('Template.IntractTemplate');
        }
        export const deletePermission = 'Template:IntractTemplate';
        export const insertPermission = 'Template:IntractTemplate';
        export const readPermission = 'Template:IntractTemplate';
        export const updatePermission = 'Template:IntractTemplate';

        export declare const enum Fields {
            Id = "Id",
            CreatedAtUtc = "CreatedAtUtc",
            Name = "Name",
            IntractId = "IntractId",
            Language = "Language",
            Category = "Category",
            TemplateCategoryLabel = "TemplateCategoryLabel",
            HeaderFormat = "HeaderFormat",
            Header = "Header",
            Body = "Body",
            Footer = "Footer",
            Buttons = "Buttons",
            AutosubmittedFor = "AutosubmittedFor",
            DisplayName = "DisplayName",
            ApprovalStatus = "ApprovalStatus",
            WaTemplateId = "WaTemplateId",
            VariablePresent = "VariablePresent",
            header_handle_file_url = "header_handle_file_url"
        }
    }
}
