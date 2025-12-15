namespace AdvanceCRM.Common {
    export interface QuickSMSForm {
        Number: Serenity.MaskedEditor;
        Message: Serenity.TextAreaEditor;
        TemplateID: Serenity.StringEditor;
    }

    export class QuickSMSForm extends Serenity.PrefixedContext {
        static formKey = 'Common.QuickSMS';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuickSMSForm.init)  {
                QuickSMSForm.init = true;

                var s = Serenity;
                var w0 = s.MaskedEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.StringEditor;

                Q.initFormType(QuickSMSForm, [
                    'Number', w0,
                    'Message', w1,
                    'TemplateID', w2
                ]);
            }
        }
    }
}
