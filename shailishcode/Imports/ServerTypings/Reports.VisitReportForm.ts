namespace AdvanceCRM.Reports {
    export interface VisitReportForm {
        Type: Serenity.EnumEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        Representative: Administration.UserEditor;
    }

    export class VisitReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.VisitReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!VisitReportForm.init)  {
                VisitReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(VisitReportForm, [
                    'Type', w0,
                    'DateFrom', w1,
                    'DateTo', w1,
                    'Representative', w2
                ]);
            }
        }
    }
}
