namespace AdvanceCRM.ThirdParty {
    export interface TradeIndiaDetailsForm {
        RfiId: Serenity.IntegerEditor;
        Source: Serenity.StringEditor;
        ProductSource: Serenity.StringEditor;
        GeneratedDateTime: Serenity.DateTimeEditor;
        InquiryType: Serenity.StringEditor;
        ProductName: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        OrderValueMin: Serenity.IntegerEditor;
        MonthSlot: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        Message: Serenity.TextAreaEditor;
        SenderCo: Serenity.StringEditor;
        SenderName: Serenity.StringEditor;
        SenderMobile: Serenity.StringEditor;
        SenderEmail: Serenity.StringEditor;
        SenderAddress: Serenity.StringEditor;
        SenderCity: Serenity.StringEditor;
        SenderState: Serenity.StringEditor;
        SenderCountry: Serenity.StringEditor;
        LandlineNumber: Serenity.StringEditor;
        PrefSuppLocation: Serenity.StringEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class TradeIndiaDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.TradeIndiaDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TradeIndiaDetailsForm.init)  {
                TradeIndiaDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.BooleanEditor;

                Q.initFormType(TradeIndiaDetailsForm, [
                    'RfiId', w0,
                    'Source', w1,
                    'ProductSource', w1,
                    'GeneratedDateTime', w2,
                    'InquiryType', w1,
                    'ProductName', w1,
                    'Quantity', w0,
                    'OrderValueMin', w0,
                    'MonthSlot', w1,
                    'Subject', w1,
                    'Message', w3,
                    'SenderCo', w1,
                    'SenderName', w1,
                    'SenderMobile', w1,
                    'SenderEmail', w1,
                    'SenderAddress', w1,
                    'SenderCity', w1,
                    'SenderState', w1,
                    'SenderCountry', w1,
                    'LandlineNumber', w1,
                    'PrefSuppLocation', w1,
                    'Feedback', w3,
                    'IsMoved', w4
                ]);
            }
        }
    }
}
