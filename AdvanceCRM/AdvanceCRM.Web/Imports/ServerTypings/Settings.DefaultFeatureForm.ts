namespace AdvanceCRM.Settings {
    export interface DefaultFeatureForm {
        Name: Serenity.StringEditor;
    }

    export class DefaultFeatureForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.DefaultFeature';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DefaultFeatureForm.init)  {
                DefaultFeatureForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DefaultFeatureForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
