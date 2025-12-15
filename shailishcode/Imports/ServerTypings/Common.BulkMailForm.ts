namespace AdvanceCRM.Common {
    export interface BulkMailForm {
        Subject: Serenity.StringEditor;
        Attach: Serenity.StringEditor;
        Message: Serenity.TextAreaEditor;
    }

    export class BulkMailForm extends Serenity.PrefixedContext {
        static formKey = 'Common.BulkMail';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BulkMailForm.init)  {
                BulkMailForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;

                Q.initFormType(BulkMailForm, [
                    'Subject', w0,
                    'Attach', w0,
                    'Message', w1
                ]);
            }
        }
    }
}
