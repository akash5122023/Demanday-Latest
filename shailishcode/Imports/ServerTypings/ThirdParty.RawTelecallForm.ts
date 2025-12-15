namespace AdvanceCRM.ThirdParty {
    export interface RawTelecallForm {
        CompanyName: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Details: Serenity.TextAreaEditor;
        Feedback: Serenity.TextAreaEditor;
        CreatedBy: Administration.UserEditor;
        AssignedTo: Administration.UserEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class RawTelecallForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.RawTelecall';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!RawTelecallForm.init)  {
                RawTelecallForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = Administration.UserEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(RawTelecallForm, [
                    'CompanyName', w0,
                    'Name', w0,
                    'Phone', w0,
                    'Email', w0,
                    'Details', w1,
                    'Feedback', w1,
                    'CreatedBy', w2,
                    'AssignedTo', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
