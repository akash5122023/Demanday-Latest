namespace AdvanceCRM.Reports {
    export interface SignInReportForm {
        Representative: Administration.UserEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
    }

    export class SignInReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.SignInReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SignInReportForm.init)  {
                SignInReportForm.init = true;

                var s = Serenity;
                var w0 = Administration.UserEditor;
                var w1 = s.DateEditor;

                Q.initFormType(SignInReportForm, [
                    'Representative', w0,
                    'DateFrom', w1,
                    'DateTo', w1
                ]);
            }
        }
    }
}
