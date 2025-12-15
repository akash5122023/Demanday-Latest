namespace AdvanceCRM.ThirdParty {
    export interface VisitForm {
        CompanyName: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        MobileNo: Serenity.StringEditor;
        Location: Serenity.StringEditor;
        DateNTime: Serenity.DateTimeEditor;
        Requirements: Serenity.StringEditor;
        Purpose: Serenity.StringEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
        CreatedBy: Administration.UserEditor;
    }

    export class VisitForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.Visit';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!VisitForm.init)  {
                VisitForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.MultipleImageUploadEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.BooleanEditor;
                var w5 = Administration.UserEditor;

                Q.initFormType(VisitForm, [
                    'CompanyName', w0,
                    'Name', w0,
                    'Address', w0,
                    'Email', w0,
                    'MobileNo', w0,
                    'Location', w0,
                    'DateNTime', w1,
                    'Requirements', w0,
                    'Purpose', w0,
                    'Attachments', w2,
                    'Feedback', w3,
                    'IsMoved', w4,
                    'CreatedBy', w5
                ]);
            }
        }
    }
}
