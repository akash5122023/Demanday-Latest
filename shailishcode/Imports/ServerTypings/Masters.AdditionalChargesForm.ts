namespace AdvanceCRM.Masters {
    export interface AdditionalChargesForm {
        Name: Serenity.StringEditor;
        Percentage: Serenity.DecimalEditor;
    }

    export class AdditionalChargesForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.AdditionalCharges';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AdditionalChargesForm.init)  {
                AdditionalChargesForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;

                Q.initFormType(AdditionalChargesForm, [
                    'Name', w0,
                    'Percentage', w1
                ]);
            }
        }
    }
}
