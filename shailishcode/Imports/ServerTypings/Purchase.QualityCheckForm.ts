namespace AdvanceCRM.Purchase {
    export interface QualityCheckForm {
        QcNumber: Serenity.IntegerEditor;
        PurchaseDate: Serenity.DateEditor;
        PurchaseFromId: Serenity.LookupEditor;
        ProductId: Serenity.LookupEditor;
        QcDate: Serenity.DateEditor;
        InspectionCriteria: Serenity.StringEditor;
        QtyInspected: Serenity.IntegerEditor;
        QtyPassed: Serenity.IntegerEditor;
        QtyRejected: Serenity.IntegerEditor;
        DepositionAction: Serenity.StringEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
    }

    export class QualityCheckForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.QualityCheck';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QualityCheckForm.init)  {
                QualityCheckForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.DateEditor;
                var w2 = s.LookupEditor;
                var w3 = s.StringEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.MultipleImageUploadEditor;

                Q.initFormType(QualityCheckForm, [
                    'QcNumber', w0,
                    'PurchaseDate', w1,
                    'PurchaseFromId', w2,
                    'ProductId', w2,
                    'QcDate', w1,
                    'InspectionCriteria', w3,
                    'QtyInspected', w0,
                    'QtyPassed', w0,
                    'QtyRejected', w0,
                    'DepositionAction', w3,
                    'AdditionalInfo', w4,
                    'Attachments', w5
                ]);
            }
        }
    }
}
