namespace AdvanceCRM.Masters {
    export interface TaxForm {
        Name: Serenity.StringEditor;
        Type: Serenity.StringEditor;
        Percentage: Serenity.DecimalEditor;
    }

    export class TaxForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Tax';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TaxForm.init)  {
                TaxForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;

                Q.initFormType(TaxForm, [
                    'Name', w0,
                    'Type', w0,
                    'Percentage', w1
                ]);
            }
        }
    }
}
