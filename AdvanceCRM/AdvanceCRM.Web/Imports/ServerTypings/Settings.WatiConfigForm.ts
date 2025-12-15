namespace AdvanceCRM.Settings {
    export interface WatiConfigForm {
        Url: Serenity.StringEditor;
        Token: Serenity.StringEditor;
    }

    export class WatiConfigForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.WatiConfig';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WatiConfigForm.init)  {
                WatiConfigForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(WatiConfigForm, [
                    'Url', w0,
                    'Token', w0
                ]);
            }
        }
    }
}
