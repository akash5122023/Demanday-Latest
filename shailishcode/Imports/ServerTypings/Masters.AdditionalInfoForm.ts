namespace AdvanceCRM.Masters {
    export interface AdditionalInfoForm {
        AdditionalInfo: Serenity.StringEditor;
        Type: Serenity.EnumEditor;
    }

    export class AdditionalInfoForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.AdditionalInfo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AdditionalInfoForm.init)  {
                AdditionalInfoForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;

                Q.initFormType(AdditionalInfoForm, [
                    'AdditionalInfo', w0,
                    'Type', w1
                ]);
            }
        }
    }
}
