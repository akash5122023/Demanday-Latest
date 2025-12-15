namespace AdvanceCRM.Settings {
    export interface BulkMailConfigForm {
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        Ssl: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.StringEditor;
    }

    export class BulkMailConfigForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.BulkMailConfig';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BulkMailConfigForm.init)  {
                BulkMailConfigForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.BooleanEditor;

                Q.initFormType(BulkMailConfigForm, [
                    'Host', w0,
                    'Port', w1,
                    'Ssl', w2,
                    'EmailId', w0,
                    'EmailPassword', w0
                ]);
            }
        }
    }
}
