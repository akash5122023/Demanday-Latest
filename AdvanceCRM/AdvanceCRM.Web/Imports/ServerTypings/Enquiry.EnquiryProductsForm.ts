namespace AdvanceCRM.Enquiry {
    export interface EnquiryProductsForm {
        ProductsId: Serenity.LookupEditor;
        Capacity: Serenity.StringEditor;
        Code: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Unit: Serenity.StringEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Discount: Serenity.DecimalEditor;
        Description: Serenity.TextAreaEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Destination: Serenity.StringEditor;
        Nights: Serenity.StringEditor;
        Adults: Serenity.StringEditor;
        Childrens: Serenity.StringEditor;
        HotelName: Serenity.StringEditor;
        MealPlan: Serenity.StringEditor;
    }

    export class EnquiryProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Enquiry.EnquiryProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryProductsForm.init)  {
                EnquiryProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.DateEditor;

                Q.initFormType(EnquiryProductsForm, [
                    'ProductsId', w0,
                    'Capacity', w1,
                    'Code', w0,
                    'Quantity', w2,
                    'Mrp', w2,
                    'Unit', w1,
                    'SellingPrice', w2,
                    'Price', w2,
                    'Discount', w2,
                    'Description', w3,
                    'From', w1,
                    'To', w1,
                    'Date', w4,
                    'Destination', w1,
                    'Nights', w1,
                    'Adults', w1,
                    'Childrens', w1,
                    'HotelName', w1,
                    'MealPlan', w1
                ]);
            }
        }
    }
}
