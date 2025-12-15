namespace AdvanceCRM.ThirdParty {
    export interface WebsiteEnquiryForm {
        Name: Serenity.StringEditor;
        DateTime: Serenity.DateTimeEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Address: Serenity.TextAreaEditor;
        Requirement: Serenity.TextAreaEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class WebsiteEnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WebsiteEnquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WebsiteEnquiryForm.init)  {
                WebsiteEnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(WebsiteEnquiryForm, [
                    'Name', w0,
                    'DateTime', w1,
                    'Phone', w0,
                    'Email', w0,
                    'Address', w2,
                    'Requirement', w2,
                    'Feedback', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
