namespace AdvanceCRM.Settings {
    export interface WaConfigrationForm {
        Mobile: Serenity.StringEditor;
        ApiKey: Serenity.StringEditor;
        MessageApi: Serenity.StringEditor;
        MediaApi: Serenity.StringEditor;
        SuccessResponse: Serenity.StringEditor;
    }

    export class WaConfigrationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.WaConfigration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WaConfigrationForm.init)  {
                WaConfigrationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(WaConfigrationForm, [
                    'Mobile', w0,
                    'ApiKey', w0,
                    'MessageApi', w0,
                    'MediaApi', w0,
                    'SuccessResponse', w0
                ]);
            }
        }
    }
}
