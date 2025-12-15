namespace AdvanceCRM.ThirdParty {
    export interface InstamojoDetailsForm {
        InstaId: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Purpose: Serenity.StringEditor;
        PaymentMode: Serenity.StringEditor;
        Status: Serenity.StringEditor;
        PayoutDate: Serenity.DateEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class InstamojoDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.InstamojoDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InstamojoDetailsForm.init)  {
                InstamojoDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.BooleanEditor;

                Q.initFormType(InstamojoDetailsForm, [
                    'InstaId', w0,
                    'Name', w0,
                    'Phone', w0,
                    'Address', w0,
                    'Email', w0,
                    'Purpose', w0,
                    'PaymentMode', w0,
                    'Status', w0,
                    'PayoutDate', w1,
                    'IsMoved', w2
                ]);
            }
        }
    }
}
