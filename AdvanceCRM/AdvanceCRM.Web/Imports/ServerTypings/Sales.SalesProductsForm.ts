namespace AdvanceCRM.Sales {
    export interface SalesProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
        Serial: Serenity.StringEditor;
        Batch: Serenity.StringEditor;
        Quantity: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Inclusive: BooleanSwitchEditor;
        Description: Serenity.TextAreaEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Destination: Serenity.StringEditor;
        HotelName: Serenity.StringEditor;
        Nights: Serenity.StringEditor;
        Adults: Serenity.StringEditor;
        Childrens: Serenity.StringEditor;
        MealPlan: Serenity.StringEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
        WarrantyStart: Serenity.DateEditor;
        WarrantyEnd: Serenity.DateEditor;
    }

    export class SalesProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.SalesProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SalesProductsForm.init)  {
                SalesProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.DateEditor;

                Q.initFormType(SalesProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'Serial', w1,
                    'Batch', w1,
                    'Quantity', w2,
                    'Mrp', w2,
                    'Unit', w1,
                    'SellingPrice', w2,
                    'Price', w2,
                    'Inclusive', w3,
                    'Description', w4,
                    'From', w1,
                    'To', w1,
                    'Date', w5,
                    'Destination', w1,
                    'HotelName', w1,
                    'Nights', w1,
                    'Adults', w1,
                    'Childrens', w1,
                    'MealPlan', w1,
                    'Discount', w2,
                    'DiscountAmount', w2,
                    'TaxType1', w1,
                    'Percentage1', w2,
                    'TaxType2', w1,
                    'Percentage2', w2,
                    'WarrantyStart', w5,
                    'WarrantyEnd', w5
                ]);
            }
        }
    }
}
