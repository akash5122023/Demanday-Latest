namespace AdvanceCRM.Template {
    export interface QuickMailTemplateForm {
        Name: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        Message: Serenity.HtmlContentEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
    }

    export class QuickMailTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.QuickMailTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuickMailTemplateForm.init)  {
                QuickMailTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.MultipleImageUploadEditor;

                Q.initFormType(QuickMailTemplateForm, [
                    'Name', w0,
                    'Subject', w0,
                    'Message', w1,
                    'Attachments', w2
                ]);
            }
        }
    }
}
