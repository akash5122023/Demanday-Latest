namespace AdvanceCRM.ThirdParty {
    export interface RpPaymentDetailsForm {
        PaymentId: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Entity: Serenity.StringEditor;
        Amount: Serenity.StringEditor;
        Currency: Serenity.StringEditor;
        Status: Serenity.StringEditor;
        OrderId: Serenity.StringEditor;
        InvoiceId: Serenity.StringEditor;
        International: Serenity.StringEditor;
        Method: Serenity.StringEditor;
        RefundedAmt: Serenity.StringEditor;
        RefundStatus: Serenity.StringEditor;
        Captured: Serenity.StringEditor;
        Discription: Serenity.StringEditor;
        CardId: Serenity.StringEditor;
        Bank: Serenity.StringEditor;
        Wallet: Serenity.StringEditor;
        Vpa: Serenity.StringEditor;
        CreatedDate: Serenity.DateEditor;
        IsMoved: Serenity.BooleanEditor;
        CompanyId: Serenity.IntegerEditor;
    }

    export class RpPaymentDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.RpPaymentDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!RpPaymentDetailsForm.init)  {
                RpPaymentDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.BooleanEditor;
                var w3 = s.IntegerEditor;

                Q.initFormType(RpPaymentDetailsForm, [
                    'PaymentId', w0,
                    'Name', w0,
                    'Phone', w0,
                    'Email', w0,
                    'Entity', w0,
                    'Amount', w0,
                    'Currency', w0,
                    'Status', w0,
                    'OrderId', w0,
                    'InvoiceId', w0,
                    'International', w0,
                    'Method', w0,
                    'RefundedAmt', w0,
                    'RefundStatus', w0,
                    'Captured', w0,
                    'Discription', w0,
                    'CardId', w0,
                    'Bank', w0,
                    'Wallet', w0,
                    'Vpa', w0,
                    'CreatedDate', w1,
                    'IsMoved', w2,
                    'CompanyId', w3
                ]);
            }
        }
    }
}
