namespace AdvanceCRM.Reports {
    export interface AttendanceReportForm {
        Type: Serenity.EnumEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        Representative: Administration.UserEditor;
    }

    export class AttendanceReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.AttendanceReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AttendanceReportForm.init)  {
                AttendanceReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(AttendanceReportForm, [
                    'Type', w0,
                    'DateFrom', w1,
                    'DateTo', w1,
                    'Representative', w2
                ]);
            }
        }
    }
}
