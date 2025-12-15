namespace AdvanceCRM.Quotation {
    export interface QuotationProductsForm {
        ProductsId: Serenity.LookupEditor;
        Capacity: Serenity.StringEditor;
        Code: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        ProductsDivision: Serenity.StringEditor;
        Inclusive: BooleanSwitchEditor;
        Discount: Serenity.DecimalEditor;
        DiscountAmount: Serenity.DecimalEditor;
        FileAttachment: Serenity.ImageUploadEditor;
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

    export class QuotationProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Quotation.QuotationProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuotationProductsForm.init)  {
                QuotationProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.ImageUploadEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.DateEditor;

                Q.initFormType(QuotationProductsForm, [
                    'ProductsId', w0,
                    'Capacity', w1,
                    'Code', w0,
                    'Quantity', w2,
                    'Mrp', w2,
                    'Unit', w1,
                    'SellingPrice', w2,
                    'Price', w2,
                    'ProductsDivision', w1,
                    'Inclusive', w3,
                    'Discount', w2,
                    'DiscountAmount', w2,
                    'FileAttachment', w4,
                    'Description', w5,
                    'From', w1,
                    'To', w1,
                    'Date', w6,
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
