namespace AdvanceCRM.Reports {
    export interface EnquiryProductsForm {
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        SellingPrice: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        Discount: Serenity.DecimalEditor;
        EnquiryId: Serenity.IntegerEditor;
        Description: Serenity.StringEditor;
        Capacity: Serenity.StringEditor;
    }

    export class EnquiryProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.EnquiryProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryProductsForm.init)  {
                EnquiryProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.IntegerEditor;
                var w3 = s.StringEditor;

                Q.initFormType(EnquiryProductsForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'Mrp', w1,
                    'SellingPrice', w1,
                    'Price', w1,
                    'Discount', w1,
                    'EnquiryId', w2,
                    'Description', w3,
                    'Capacity', w3
                ]);
            }
        }
    }
}
