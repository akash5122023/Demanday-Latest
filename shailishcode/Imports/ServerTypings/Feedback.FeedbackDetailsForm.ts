namespace AdvanceCRM.Feedback {
    export interface FeedbackDetailsForm {
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Service: Serenity.IntegerEditor;
        Refer: Serenity.BooleanEditor;
        Details: Serenity.StringEditor;
    }

    export class FeedbackDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'Feedback.FeedbackDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!FeedbackDetailsForm.init)  {
                FeedbackDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.BooleanEditor;

                Q.initFormType(FeedbackDetailsForm, [
                    'Name', w0,
                    'Phone', w0,
                    'Service', w1,
                    'Refer', w2,
                    'Details', w0
                ]);
            }
        }
    }
}
