namespace AdvanceCRM.Common {
    export interface QuickBizEmailForm {
        Subject: Serenity.StringEditor;
        Message: Serenity.HtmlNoteContentEditor;
        Date: Serenity.DateTimeEditor;
    }

    export class QuickBizEmailForm extends Serenity.PrefixedContext {
        static formKey = 'Common.QuickBizEmail';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuickBizEmailForm.init)  {
                QuickBizEmailForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlNoteContentEditor;
                var w2 = s.DateTimeEditor;

                Q.initFormType(QuickBizEmailForm, [
                    'Subject', w0,
                    'Message', w1,
                    'Date', w2
                ]);
            }
        }
    }
}
