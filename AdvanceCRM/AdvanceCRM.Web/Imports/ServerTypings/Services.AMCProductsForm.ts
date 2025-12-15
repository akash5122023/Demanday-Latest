namespace AdvanceCRM.Services {
    export interface AMCProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
        SerialNo: Serenity.StringEditor;
        Rate: Serenity.DecimalEditor;
        Inclusive: BooleanSwitchEditor;
        Type: Serenity.EnumEditor;
        Quantity: Serenity.IntegerEditor;
        Visits: Serenity.IntegerEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
    }

    export class AMCProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Services.AMCProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AMCProductsForm.init)  {
                AMCProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.EnumEditor;
                var w5 = s.IntegerEditor;

                Q.initFormType(AMCProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'SerialNo', w1,
                    'Rate', w2,
                    'Inclusive', w3,
                    'Type', w4,
                    'Quantity', w5,
                    'Visits', w5,
                    'Discount', w2,
                    'DiscountAmount', w2,
                    'TaxType1', w1,
                    'Percentage1', w2,
                    'TaxType2', w1,
                    'Percentage2', w2
                ]);
            }
        }
    }
}
