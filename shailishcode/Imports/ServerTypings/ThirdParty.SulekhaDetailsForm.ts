namespace AdvanceCRM.ThirdParty {
    export interface SulekhaDetailsForm {
        DateTime: Serenity.DateTimeEditor;
        UserName: Serenity.StringEditor;
        Mobile: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        City: Serenity.StringEditor;
        Localities: Serenity.StringEditor;
        Comments: Serenity.StringEditor;
        Keywords: Serenity.StringEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class SulekhaDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.SulekhaDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SulekhaDetailsForm.init)  {
                SulekhaDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.DateTimeEditor;
                var w1 = s.StringEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(SulekhaDetailsForm, [
                    'DateTime', w0,
                    'UserName', w1,
                    'Mobile', w1,
                    'Email', w1,
                    'City', w1,
                    'Localities', w1,
                    'Comments', w1,
                    'Keywords', w1,
                    'Feedback', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
