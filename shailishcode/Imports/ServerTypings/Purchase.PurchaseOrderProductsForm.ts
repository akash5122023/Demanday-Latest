namespace AdvanceCRM.Purchase {
    export interface PurchaseOrderProductsForm {
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Inclusive: BooleanSwitchEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
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

    export class PurchaseOrderProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.PurchaseOrderProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseOrderProductsForm.init)  {
                PurchaseOrderProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = BooleanSwitchEditor;
                var w3 = s.StringEditor;
                var w4 = s.DateEditor;

                Q.initFormType(PurchaseOrderProductsForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'Price', w1,
                    'Inclusive', w2,
                    'Discount', w1,
                    'DiscountAmount', w1,
                    'Unit', w3,
                    'From', w3,
                    'To', w3,
                    'Date', w4,
                    'Destination', w3,
                    'HotelName', w3,
                    'Nights', w3,
                    'Adults', w3,
                    'Childrens', w3,
                    'MealPlan', w3,
                    'TaxType1', w3,
                    'Percentage1', w1,
                    'TaxType2', w3,
                    'Percentage2', w1
                ]);
            }
        }
    }
}
