namespace AdvanceCRM.Membership {
    export interface SignUpForm {
        Plan: Serenity.StringEditor;
        Company: Serenity.StringEditor;
        Subdomain: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        ConfirmEmail: Serenity.EmailEditor;
        MobileNumber: Serenity.StringEditor;
        AreaCode: Serenity.StringEditor;
        Password: Serenity.StringEditor;
        ConfirmPassword: Serenity.StringEditor;
        PaymentOrderId: Serenity.StringEditor;
        PaymentId: Serenity.StringEditor;
        PaymentSignature: Serenity.StringEditor;
        PaymentAmount: Serenity.StringEditor;
        PaymentCurrency: Serenity.StringEditor;
        Users: Serenity.IntegerEditor;
        CouponCode: Serenity.StringEditor;
    }

    export class SignUpForm extends Serenity.PrefixedContext {
        static formKey = 'Membership.SignUp';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SignUpForm.init)  {
                SignUpForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EmailEditor;
                var w2 = s.IntegerEditor;

                Q.initFormType(SignUpForm, [
                    'Plan', w0,
                    'Company', w0,
                    'Subdomain', w0,
                    'DisplayName', w0,
                    'Email', w1,
                    'ConfirmEmail', w1,
                    'MobileNumber', w0,
                    'AreaCode', w0,
                    'Password', w0,
                    'ConfirmPassword', w0,
                    'PaymentOrderId', w0,
                    'PaymentId', w0,
                    'PaymentSignature', w0,
                    'PaymentAmount', w0,
                    'PaymentCurrency', w0,
                    'Users', w2,
                    'CouponCode', w0
                ]);
            }
        }
    }
}
