namespace AdvanceCRM.Sales {
    export interface InwardProductsForm {
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
        BranchId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class InwardProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.InwardProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InwardProductsForm.init)  {
                InwardProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(InwardProductsForm, [
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
                    'BranchId', w0,
                    'Description', w3
                ]);
            }
        }
    }
}
