namespace AdvanceCRM.Masters {
    export interface ProductsDivisionForm {
        ProductsDivision: Serenity.StringEditor;
    }

    export class ProductsDivisionForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ProductsDivision';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductsDivisionForm.init)  {
                ProductsDivisionForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ProductsDivisionForm, [
                    'ProductsDivision', w0
                ]);
            }
        }
    }
}
