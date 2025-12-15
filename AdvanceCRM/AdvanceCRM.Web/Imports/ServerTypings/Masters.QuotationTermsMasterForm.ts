namespace AdvanceCRM.Masters {
    export interface QuotationTermsMasterForm {
        Terms: Serenity.TextAreaEditor;
    }

    export class QuotationTermsMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.QuotationTermsMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuotationTermsMasterForm.init)  {
                QuotationTermsMasterForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;

                Q.initFormType(QuotationTermsMasterForm, [
                    'Terms', w0
                ]);
            }
        }
    }
}
