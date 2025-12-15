namespace AdvanceCRM.Sales {
    export interface OutwardProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
        Serial: Serenity.StringEditor;
        Batch: Serenity.StringEditor;
        Quantity: Serenity.DecimalEditor;
        SellingPrice: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        Price: Serenity.DecimalEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        BranchId: Serenity.IntegerEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class OutwardProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.OutwardProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!OutwardProductsForm.init)  {
                OutwardProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.TextAreaEditor;

                Q.initFormType(OutwardProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'Serial', w1,
                    'Batch', w1,
                    'Quantity', w2,
                    'SellingPrice', w2,
                    'Mrp', w2,
                    'Unit', w1,
                    'Price', w2,
                    'Discount', w2,
                    'DiscountAmount', w2,
                    'BranchId', w3,
                    'Description', w4
                ]);
            }
        }
    }
}
