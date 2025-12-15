namespace AdvanceCRM.Settings {
    export interface BizMailConfigForm {
        Apiurl: Serenity.StringEditor;
        Apikey: Serenity.StringEditor;
        FromName: Serenity.StringEditor;
        FromMail: Serenity.StringEditor;
        ReplyToName: Serenity.StringEditor;
        ReplyToMail: Serenity.StringEditor;
    }

    export class BizMailConfigForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.BizMailConfig';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailConfigForm.init)  {
                BizMailConfigForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(BizMailConfigForm, [
                    'Apiurl', w0,
                    'Apikey', w0,
                    'FromName', w0,
                    'FromMail', w0,
                    'ReplyToName', w0,
                    'ReplyToMail', w0
                ]);
            }
        }
    }
}
