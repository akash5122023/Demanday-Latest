namespace AdvanceCRM.Common {
    export interface QuickMailForm {
        EmailTo: Serenity.StringEditor;
        TemplateId: Serenity.LookupEditor;
        Subject: Serenity.StringEditor;
        Message: Serenity.HtmlContentEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
    }

    export class QuickMailForm extends Serenity.PrefixedContext {
        static formKey = 'Common.QuickMail';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuickMailForm.init)  {
                QuickMailForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.HtmlContentEditor;
                var w3 = s.MultipleImageUploadEditor;

                Q.initFormType(QuickMailForm, [
                    'EmailTo', w0,
                    'TemplateId', w1,
                    'Subject', w0,
                    'Message', w2,
                    'Attachments', w3
                ]);
            }
        }
    }
}
