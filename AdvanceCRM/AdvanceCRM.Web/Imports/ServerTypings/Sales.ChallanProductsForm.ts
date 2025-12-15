namespace AdvanceCRM.Sales {
    export interface ChallanProductsForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.LookupEditor;
        Serial: Serenity.StringEditor;
        Batch: Serenity.StringEditor;
        Quantity: Serenity.DecimalEditor;
        SellingPrice: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        Price: Serenity.DecimalEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Destination: Serenity.StringEditor;
        HotelName: Serenity.StringEditor;
        Nights: Serenity.StringEditor;
        Adults: Serenity.StringEditor;
        Childrens: Serenity.StringEditor;
        MealPlan: Serenity.StringEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class ChallanProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.ChallanProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ChallanProductsForm.init)  {
                ChallanProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.DateEditor;
                var w4 = s.TextAreaEditor;

                Q.initFormType(ChallanProductsForm, [
                    'ProductsId', w0,
                    'Code', w0,
                    'Serial', w1,
                    'Batch', w1,
                    'Quantity', w2,
                    'SellingPrice', w2,
                    'Mrp', w2,
                    'Unit', w1,
                    'Price', w2,
                    'From', w1,
                    'To', w1,
                    'Date', w3,
                    'Destination', w1,
                    'HotelName', w1,
                    'Nights', w1,
                    'Adults', w1,
                    'Childrens', w1,
                    'MealPlan', w1,
                    'TaxType1', w1,
                    'Percentage1', w2,
                    'TaxType2', w1,
                    'Percentage2', w2,
                    'Discount', w2,
                    'DiscountAmount', w2,
                    'Description', w4
                ]);
            }
        }
    }
}
