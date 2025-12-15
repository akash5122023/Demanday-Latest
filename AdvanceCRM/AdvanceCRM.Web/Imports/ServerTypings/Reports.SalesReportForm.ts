namespace AdvanceCRM.Reports {
    export interface SalesReportForm {
        Type: Serenity.EnumEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        Representative: Administration.UserEditor;
        Contact: Serenity.LookupEditor;
    }

    export class SalesReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.SalesReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SalesReportForm.init)  {
                SalesReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateEditor;
                var w2 = Administration.UserEditor;
                var w3 = s.LookupEditor;

                Q.initFormType(SalesReportForm, [
                    'Type', w0,
                    'DateFrom', w1,
                    'DateTo', w1,
                    'Representative', w2,
                    'Contact', w3
                ]);
            }
        }
    }
}
