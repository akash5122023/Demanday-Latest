namespace AdvanceCRM.Products {
    export interface BomProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        BomBranchId: Serenity.LookupEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Inclusive: BooleanSwitchEditor;
        Description: Serenity.TextAreaEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
        WarrantyStart: Serenity.DateEditor;
        WarrantyEnd: Serenity.DateEditor;
        BomId: Serenity.IntegerEditor;
    }

    export class BomProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Products.BomProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BomProductsForm.init)  {
                BomProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.DateEditor;
                var w6 = s.IntegerEditor;

                Q.initFormType(BomProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'Quantity', w1,
                    'BomBranchId', w0,
                    'Mrp', w1,
                    'Unit', w2,
                    'SellingPrice', w1,
                    'Price', w1,
                    'Inclusive', w3,
                    'Description', w4,
                    'Discount', w1,
                    'DiscountAmount', w1,
                    'TaxType1', w2,
                    'Percentage1', w1,
                    'TaxType2', w2,
                    'Percentage2', w1,
                    'WarrantyStart', w5,
                    'WarrantyEnd', w5,
                    'BomId', w6
                ]);
            }
        }
    }
}
