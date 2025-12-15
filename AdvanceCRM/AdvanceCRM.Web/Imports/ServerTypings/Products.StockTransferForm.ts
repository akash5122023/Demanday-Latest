namespace AdvanceCRM.Products {
    export interface StockTransferForm {
        Date: Serenity.DateEditor;
        RepresentativeId: Administration.UserEditor;
        FromBranchId: Serenity.LookupEditor;
        ToBranchId: Serenity.LookupEditor;
        Products: StockTransferProductsEditor;
        TotalQty: Serenity.DecimalEditor;
        Amount: Serenity.DecimalEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
    }

    export class StockTransferForm extends Serenity.PrefixedContext {
        static formKey = 'Products.StockTransfer';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!StockTransferForm.init)  {
                StockTransferForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = Administration.UserEditor;
                var w2 = s.LookupEditor;
                var w3 = StockTransferProductsEditor;
                var w4 = s.DecimalEditor;
                var w5 = s.TextAreaEditor;

                Q.initFormType(StockTransferForm, [
                    'Date', w0,
                    'RepresentativeId', w1,
                    'FromBranchId', w2,
                    'ToBranchId', w2,
                    'Products', w3,
                    'TotalQty', w4,
                    'Amount', w4,
                    'AdditionalInfo', w5
                ]);
            }
        }
    }
}
