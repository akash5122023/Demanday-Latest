namespace AdvanceCRM.Settings {
    export interface AiConfigurationForm {
        AiKey: Serenity.StringEditor;
    }

    export class AiConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.AiConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AiConfigurationForm.init)  {
                AiConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(AiConfigurationForm, [
                    'AiKey', w0
                ]);
            }
        }
    }
}
