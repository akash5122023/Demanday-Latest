namespace AdvanceCRM.Template {
    export interface IntractTemplateForm {
        CreatedAtUtc: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        IntractId: Serenity.StringEditor;
        Language: Serenity.StringEditor;
        Category: Serenity.StringEditor;
        TemplateCategoryLabel: Serenity.StringEditor;
        HeaderFormat: Serenity.StringEditor;
        Header: Serenity.StringEditor;
        Body: Serenity.StringEditor;
        Footer: Serenity.StringEditor;
        Buttons: Serenity.StringEditor;
        AutosubmittedFor: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        ApprovalStatus: Serenity.StringEditor;
        WaTemplateId: Serenity.StringEditor;
        VariablePresent: Serenity.StringEditor;
        header_handle_file_url: Serenity.StringEditor;
    }

    export class IntractTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.IntractTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IntractTemplateForm.init)  {
                IntractTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(IntractTemplateForm, [
                    'CreatedAtUtc', w0,
                    'Name', w0,
                    'IntractId', w0,
                    'Language', w0,
                    'Category', w0,
                    'TemplateCategoryLabel', w0,
                    'HeaderFormat', w0,
                    'Header', w0,
                    'Body', w0,
                    'Footer', w0,
                    'Buttons', w0,
                    'AutosubmittedFor', w0,
                    'DisplayName', w0,
                    'ApprovalStatus', w0,
                    'WaTemplateId', w0,
                    'VariablePresent', w0,
                    'header_handle_file_url', w0
                ]);
            }
        }
    }
}
