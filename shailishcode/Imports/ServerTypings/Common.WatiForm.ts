namespace AdvanceCRM.Common {
    export interface WatiForm {
        Number: Serenity.MaskedEditor;
        Message: Serenity.TextAreaEditor;
    }

    export class WatiForm extends Serenity.PrefixedContext {
        static formKey = 'Common.Wati';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WatiForm.init)  {
                WatiForm.init = true;

                var s = Serenity;
                var w0 = s.MaskedEditor;
                var w1 = s.TextAreaEditor;

                Q.initFormType(WatiForm, [
                    'Number', w0,
                    'Message', w1
                ]);
            }
        }
    }
}
