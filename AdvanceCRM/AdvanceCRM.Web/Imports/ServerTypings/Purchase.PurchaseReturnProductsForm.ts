namespace AdvanceCRM.Purchase {
    export interface PurchaseReturnProductsForm {
        ProductsId: Serenity.LookupEditor;
        Serial: Serenity.StringEditor;
        Batch: Serenity.StringEditor;
        Quantity: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
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
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
    }

    export class PurchaseReturnProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.PurchaseReturnProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseReturnProductsForm.init)  {
                PurchaseReturnProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.DateEditor;

                Q.initFormType(PurchaseReturnProductsForm, [
                    'ProductsId', w0,
                    'Serial', w1,
                    'Batch', w1,
                    'Quantity', w2,
                    'Price', w2,
                    'Description', w3,
                    'From', w1,
                    'To', w1,
                    'Date', w4,
                    'Destination', w1,
                    'HotelName', w1,
                    'Nights', w1,
                    'Adults', w1,
                    'Childrens', w1,
                    'MealPlan', w1,
                    'TaxType1', w1,
                    'Percentage1', w2,
                    'TaxType2', w1,
                    'Percentage2', w2
                ]);
            }
        }
    }
}
