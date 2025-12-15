namespace AdvanceCRM.Products {
    export interface BomForm {
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Code: Serenity.StringEditor;
        Hsn: Serenity.StringEditor;
        Unit: Serenity.StringEditor;
        ContactsId: Serenity.IntegerEditor;
        Date: Serenity.DateEditor;
        Status: Serenity.IntegerEditor;
        Type: Serenity.IntegerEditor;
        Products: BomProductsEditor;
        SellingPrice: Serenity.DecimalEditor;
        Mrp: Serenity.DecimalEditor;
        Price: Serenity.DecimalEditor;
        PackagingCharges: Serenity.DecimalEditor;
        FreightCharges: Serenity.DecimalEditor;
        Advacne: Serenity.DecimalEditor;
        Roundup: Serenity.DecimalEditor;
        DueDate: Serenity.DateEditor;
        ContactPersonId: Serenity.IntegerEditor;
        QuotationNo: Serenity.IntegerEditor;
        QuotationDate: Serenity.DateEditor;
        Conversion: Serenity.DecimalEditor;
        PurchaseOrderNo: Serenity.StringEditor;
        DispatchDetails: Serenity.StringEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        Subject: Serenity.StringEditor;
        Reference: Serenity.StringEditor;
        Lines: Serenity.IntegerEditor;
        TechSpecs: Serenity.TextAreaEditor;
        Image: Serenity.ImageUploadEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        CompanyId: Serenity.IntegerEditor;
    }

    export class BomForm extends Serenity.PrefixedContext {
        static formKey = 'Products.Bom';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BomForm.init)  {
                BomForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.DateEditor;
                var w5 = BomProductsEditor;
                var w6 = s.TextAreaEditor;
                var w7 = s.MultipleImageUploadEditor;
                var w8 = s.ImageUploadEditor;
                var w9 = Administration.UserEditor;

                Q.initFormType(BomForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'Code', w2,
                    'Hsn', w2,
                    'Unit', w2,
                    'ContactsId', w3,
                    'Date', w4,
                    'Status', w3,
                    'Type', w3,
                    'Products', w5,
                    'SellingPrice', w1,
                    'Mrp', w1,
                    'Price', w1,
                    'PackagingCharges', w1,
                    'FreightCharges', w1,
                    'Advacne', w1,
                    'Roundup', w1,
                    'DueDate', w4,
                    'ContactPersonId', w3,
                    'QuotationNo', w3,
                    'QuotationDate', w4,
                    'Conversion', w1,
                    'PurchaseOrderNo', w2,
                    'DispatchDetails', w2,
                    'AdditionalInfo', w6,
                    'Attachments', w7,
                    'Subject', w2,
                    'Reference', w2,
                    'Lines', w3,
                    'TechSpecs', w6,
                    'Image', w8,
                    'OwnerId', w9,
                    'AssignedId', w9,
                    'CompanyId', w3
                ]);
            }
        }
    }
}
