namespace AdvanceCRM.Reports {
    export interface QuotationProductsForm {
        ProductsId: Serenity.IntegerEditor;
        Quantity: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Discount: Serenity.DecimalEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
        QuotationId: Serenity.IntegerEditor;
        DiscountAmount: Serenity.DecimalEditor;
        Description: Serenity.StringEditor;
        Unit: Serenity.StringEditor;
        Capacity: Serenity.StringEditor;
        ProductsDivision: Serenity.StringEditor;
    }

    export class QuotationProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.QuotationProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuotationProductsForm.init)  {
                QuotationProductsForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;

                Q.initFormType(QuotationProductsForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'Mrp', w1,
                    'SellingPrice', w1,
                    'Price', w1,
                    'Discount', w1,
                    'TaxType1', w2,
                    'Percentage1', w1,
                    'TaxType2', w2,
                    'Percentage2', w1,
                    'QuotationId', w0,
                    'DiscountAmount', w1,
                    'Description', w2,
                    'Unit', w2,
                    'Capacity', w2,
                    'ProductsDivision', w2
                ]);
            }
        }
    }
}
