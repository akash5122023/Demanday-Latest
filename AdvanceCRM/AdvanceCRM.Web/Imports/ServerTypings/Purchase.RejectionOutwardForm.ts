namespace AdvanceCRM.Purchase {
    export interface RejectionOutwardForm {
        Date: Serenity.DateEditor;
        QcNumber: Serenity.IntegerEditor;
        ProductId: Serenity.LookupEditor;
        QtyRejected: Serenity.IntegerEditor;
        PurchaseFromId: Serenity.LookupEditor;
        Status: Serenity.EnumEditor;
        ClosingDate: Serenity.DateEditor;
        BranchId: Serenity.LookupEditor;
        SentToSupplier: BooleanSwitchEditor;
        SentDate: Serenity.DateEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
    }

    export class RejectionOutwardForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.RejectionOutward';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!RejectionOutwardForm.init)  {
                RejectionOutwardForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.LookupEditor;
                var w3 = s.EnumEditor;
                var w4 = BooleanSwitchEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.MultipleImageUploadEditor;

                Q.initFormType(RejectionOutwardForm, [
                    'Date', w0,
                    'QcNumber', w1,
                    'ProductId', w2,
                    'QtyRejected', w1,
                    'PurchaseFromId', w2,
                    'Status', w3,
                    'ClosingDate', w0,
                    'BranchId', w2,
                    'SentToSupplier', w4,
                    'SentDate', w0,
                    'AdditionalInfo', w5,
                    'Attachments', w6
                ]);
            }
        }
    }
}
