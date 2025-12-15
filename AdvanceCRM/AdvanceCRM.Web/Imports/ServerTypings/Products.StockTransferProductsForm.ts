namespace AdvanceCRM.Products {
    export interface StockTransferProductsForm {
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        TransferPrice: Serenity.DecimalEditor;
        TaxType1: Serenity.StringEditor;
        Percentage1: Serenity.DecimalEditor;
        TaxType2: Serenity.StringEditor;
        Percentage2: Serenity.DecimalEditor;
    }

    export class StockTransferProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Products.StockTransferProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!StockTransferProductsForm.init)  {
                StockTransferProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;

                Q.initFormType(StockTransferProductsForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'TransferPrice', w1,
                    'TaxType1', w2,
                    'Percentage1', w1,
                    'TaxType2', w2,
                    'Percentage2', w1
                ]);
            }
        }
    }
}
