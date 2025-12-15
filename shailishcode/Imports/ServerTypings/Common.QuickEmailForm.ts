namespace AdvanceCRM.Common {
    export interface QuickEmailForm {
        Email: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        Message: Serenity.HtmlNoteContentEditor;
    }

    export class QuickEmailForm extends Serenity.PrefixedContext {
        static formKey = 'Common.QuickEmail';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuickEmailForm.init)  {
                QuickEmailForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlNoteContentEditor;

                Q.initFormType(QuickEmailForm, [
                    'Email', w0,
                    'Subject', w0,
                    'Message', w1
                ]);
            }
        }
    }
}
