namespace AdvanceCRM.Membership {
    export interface SignUpForm {
        Plan: Serenity.StringEditor;
        Company: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        ConfirmEmail: Serenity.EmailEditor;
        MobileNumber: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
        PaymentOrderId: Serenity.StringEditor;
        PaymentId: Serenity.StringEditor;
        PaymentSignature: Serenity.StringEditor;
        PaymentAmount: Serenity.StringEditor;
        PaymentCurrency: Serenity.StringEditor;
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
                var w2 = s.PasswordEditor;

                Q.initFormType(SignUpForm, [
                    'Plan', w0,
                    'Company', w0,
                    'DisplayName', w0,
                    'Email', w1,
                    'ConfirmEmail', w1,
                    'MobileNumber', w0,
                    'Password', w2,
                    'ConfirmPassword', w2,
                    'PaymentOrderId', w0,
                    'PaymentId', w0,
                    'PaymentSignature', w0,
                    'PaymentAmount', w0,
                    'PaymentCurrency', w0
                ]);
            }
        }
    }
}
