namespace AdvanceCRM.Settings {
    export interface MailChimpConfigurationForm {
        ApiKey: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Address: Serenity.TextAreaEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        Zip: Serenity.StringEditor;
        Country: Serenity.IntegerEditor;
        Reminder: Serenity.TextAreaEditor;
        ContactFromEmail: Serenity.StringEditor;
        ContactFromName: Serenity.StringEditor;
        ContactSubject: Serenity.StringEditor;
        EnquiryFromEmail: Serenity.StringEditor;
        EnquiryFromName: Serenity.StringEditor;
        EnquirySubject: Serenity.StringEditor;
        QuotationFromEmail: Serenity.StringEditor;
        QuotationFromName: Serenity.StringEditor;
        QuotationSubject: Serenity.StringEditor;
        SaleFromEmail: Serenity.StringEditor;
        SaleFromName: Serenity.StringEditor;
        SaleSubject: Serenity.StringEditor;
    }

    export class MailChimpConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.MailChimpConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MailChimpConfigurationForm.init)  {
                MailChimpConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.IntegerEditor;

                Q.initFormType(MailChimpConfigurationForm, [
                    'ApiKey', w0,
                    'CompanyName', w0,
                    'Phone', w0,
                    'Address', w1,
                    'City', w0,
                    'State', w0,
                    'Zip', w0,
                    'Country', w2,
                    'Reminder', w1,
                    'ContactFromEmail', w0,
                    'ContactFromName', w0,
                    'ContactSubject', w0,
                    'EnquiryFromEmail', w0,
                    'EnquiryFromName', w0,
                    'EnquirySubject', w0,
                    'QuotationFromEmail', w0,
                    'QuotationFromName', w0,
                    'QuotationSubject', w0,
                    'SaleFromEmail', w0,
                    'SaleFromName', w0,
                    'SaleSubject', w0
                ]);
            }
        }
    }
}
