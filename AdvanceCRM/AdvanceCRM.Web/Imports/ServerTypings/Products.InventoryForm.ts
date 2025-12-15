namespace AdvanceCRM.Products {
    export interface InventoryForm {
        Name: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        Hsn: Serenity.StringEditor;
        DivisionId: Serenity.LookupEditor;
        UnitId: Serenity.LookupEditor;
        GroupId: Serenity.LookupEditor;
        RawMaterial: BSSwitchEditor;
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        BranchId: Serenity.LookupEditor;
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

    export class InventoryForm extends Serenity.PrefixedContext {
        static formKey = 'Products.Inventory';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InventoryForm.init)  {
                InventoryForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.DecimalEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.ImageUploadEditor;

                Q.initFormType(InventoryForm, [
                    'Name', w0,
                    'Code', w0,
                    'Hsn', w0,
                    'DivisionId', w1,
                    'UnitId', w1,
                    'GroupId', w1,
                    'RawMaterial', w2,
                    'ProductsId', w1,
                    'Quantity', w3,
                    'BranchId', w1,
                    'Description', w4,
                    'SellingPrice', w3,
                    'Mrp', w3,
                    'PurchasePrice', w3,
                    'TaxId1', w1,
                    'TaxId2', w1,
                    'ChannelCustomerPrice', w3,
                    'ResellerPrice', w3,
                    'WholesalerPrice', w3,
                    'DealerPrice', w3,
                    'DistributorPrice', w3,
                    'StockiestPrice', w3,
                    'NationalDistributorPrice', w3,
                    'MinimumStock', w3,
                    'MaximumStock', w3,
                    'TechSpecs', w4,
                    'Image', w5
                ]);
            }
        }
    }
}
