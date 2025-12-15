namespace AdvanceCRM.Masters {
    export interface AdditionalConcessionForm {
        Name: Serenity.StringEditor;
        Percentage: Serenity.DecimalEditor;
        Amount: Serenity.DecimalEditor;
    }

    export class AdditionalConcessionForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.AdditionalConcession';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AdditionalConcessionForm.init)  {
                AdditionalConcessionForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;

                Q.initFormType(AdditionalConcessionForm, [
                    'Name', w0,
                    'Percentage', w1,
                    'Amount', w1
                ]);
            }
        }
    }
}
