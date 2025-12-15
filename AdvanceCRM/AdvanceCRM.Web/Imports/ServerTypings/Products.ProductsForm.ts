namespace AdvanceCRM.Products {
    export interface ProductsForm {
        Name: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        HSN: Serenity.StringEditor;
        DivisionId: Serenity.LookupEditor;
        UnitId: Serenity.LookupEditor;
        GroupId: Serenity.LookupEditor;
        OpeningStock: Serenity.DecimalEditor;
        RawMaterial: BooleanSwitchEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Destination: Serenity.StringEditor;
        Nights: Serenity.StringEditor;
        Adults: Serenity.StringEditor;
        Childrens: Serenity.StringEditor;
        HotelName: Serenity.StringEditor;
        MealPlan: Serenity.StringEditor;
        Description: Serenity.TextAreaEditor;
        SellingPrice: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        PurchasePrice: Serenity.DecimalEditor;
        TaxId1: Serenity.LookupEditor;
        TaxId2: Serenity.LookupEditor;
        ChannelCustomerPrice: Serenity.DecimalEditor;
        ResellerPrice: Serenity.DecimalEditor;
        WholesalerPrice: Serenity.DecimalEditor;
        DealerPrice: Serenity.DecimalEditor;
        DistributorPrice: Serenity.DecimalEditor;
        StockiestPrice: Serenity.DecimalEditor;
        NationalDistributorPrice: Serenity.DecimalEditor;
        MinimumStock: Serenity.DecimalEditor;
        MaximumStock: Serenity.DecimalEditor;
        TechSpecs: Serenity.TextAreaEditor;
        Image: Serenity.ImageUploadEditor;
    }

    export class ProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Products.Products';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductsForm.init)  {
                ProductsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.DecimalEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.DateEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.ImageUploadEditor;

                Q.initFormType(ProductsForm, [
                    'Name', w0,
                    'Code', w0,
                    'HSN', w0,
                    'DivisionId', w1,
                    'UnitId', w1,
                    'GroupId', w1,
                    'OpeningStock', w2,
                    'RawMaterial', w3,
                    'From', w0,
                    'To', w0,
                    'Date', w4,
                    'Destination', w0,
                    'Nights', w0,
                    'Adults', w0,
                    'Childrens', w0,
                    'HotelName', w0,
                    'MealPlan', w0,
                    'Description', w5,
                    'SellingPrice', w2,
                    'Mrp', w2,
                    'PurchasePrice', w2,
                    'TaxId1', w1,
                    'TaxId2', w1,
                    'ChannelCustomerPrice', w2,
                    'ResellerPrice', w2,
                    'WholesalerPrice', w2,
                    'DealerPrice', w2,
                    'DistributorPrice', w2,
                    'StockiestPrice', w2,
                    'NationalDistributorPrice', w2,
                    'MinimumStock', w2,
                    'MaximumStock', w2,
                    'TechSpecs', w5,
                    'Image', w6
                ]);
            }
        }
    }
}
