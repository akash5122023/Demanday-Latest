namespace AdvanceCRM.Settings {
    export interface BizWaConfigForm {
        WhatsAppNo: Serenity.StringEditor;
        PhoneNoId: Serenity.StringEditor;
        Wbaid: Serenity.StringEditor;
        Accesstoken: Serenity.TextAreaEditor;
    }

    export class BizWaConfigForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.BizWaConfig';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizWaConfigForm.init)  {
                BizWaConfigForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;

                Q.initFormType(BizWaConfigForm, [
                    'WhatsAppNo', w0,
                    'PhoneNoId', w0,
                    'Wbaid', w0,
                    'Accesstoken', w1
                ]);
            }
        }
    }
}
