namespace AdvanceCRM.Sales {
    export interface InvoiceProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
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

    export class InvoiceProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.InvoiceProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InvoiceProductsForm.init)  {
                InvoiceProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.DateEditor;

                Q.initFormType(InvoiceProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'Quantity', w1,
                    'Mrp', w1,
                    'Unit', w2,
                    'SellingPrice', w1,
                    'Price', w1,
                    'Inclusive', w3,
                    'Description', w4,
                    'From', w2,
                    'To', w2,
                    'Date', w5,
                    'Destination', w2,
                    'HotelName', w2,
                    'Nights', w2,
                    'Adults', w2,
                    'Childrens', w2,
                    'MealPlan', w2,
                    'Discount', w1,
                    'DiscountAmount', w1,
                    'TaxType1', w2,
                    'Percentage1', w1,
                    'TaxType2', w2,
                    'Percentage2', w1,
                    'WarrantyStart', w5,
                    'WarrantyEnd', w5
                ]);
            }
        }
    }
}
