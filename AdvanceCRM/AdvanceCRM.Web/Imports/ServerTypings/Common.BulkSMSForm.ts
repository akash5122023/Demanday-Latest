namespace AdvanceCRM.Common {
    export interface BulkSMSForm {
        Message: Serenity.TextAreaEditor;
        TemplateID: Serenity.StringEditor;
    }

    export class BulkSMSForm extends Serenity.PrefixedContext {
        static formKey = 'Common.BulkSMS';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BulkSMSForm.init)  {
                BulkSMSForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;
                var w1 = s.StringEditor;

                Q.initFormType(BulkSMSForm, [
                    'Message', w0,
                    'TemplateID', w1
                ]);
            }
        }
    }
}
