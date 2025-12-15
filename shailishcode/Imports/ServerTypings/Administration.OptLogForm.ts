namespace AdvanceCRM.Administration {
    export interface OptLogForm {
        Phone: Serenity.StringEditor;
        Opt: Serenity.DecimalEditor;
        Validity: Serenity.DateEditor;
    }

    export class OptLogForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.OptLog';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!OptLogForm.init)  {
                OptLogForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.DateEditor;

                Q.initFormType(OptLogForm, [
                    'Phone', w0,
                    'Opt', w1,
                    'Validity', w2
                ]);
            }
        }
    }
}
