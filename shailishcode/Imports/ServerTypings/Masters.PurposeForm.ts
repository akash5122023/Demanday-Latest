namespace AdvanceCRM.Masters {
    export interface PurposeForm {
        Purpose: Serenity.StringEditor;
    }

    export class PurposeForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Purpose';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurposeForm.init)  {
                PurposeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(PurposeForm, [
                    'Purpose', w0
                ]);
            }
        }
    }
}
