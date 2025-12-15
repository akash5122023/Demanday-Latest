namespace AdvanceCRM.ThirdParty {
    export interface WoocommerceDetailsForm {
        WooId: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Company: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        CreatedDate: Serenity.DateEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class WoocommerceDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WoocommerceDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WoocommerceDetailsForm.init)  {
                WoocommerceDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(WoocommerceDetailsForm, [
                    'WooId', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Company', w0,
                    'Email', w0,
                    'Phone', w0,
                    'Address', w0,
                    'City', w0,
                    'State', w0,
                    'Country', w0,
                    'CreatedDate', w1,
                    'Feedback', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
