namespace AdvanceCRM.Reports {
    export interface CMSReportForm {
        Type: Serenity.EnumEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        Representative: Administration.UserEditor;
        Contact: Serenity.LookupEditor;
    }

    export class CMSReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.CMSReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CMSReportForm.init)  {
                CMSReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateEditor;
                var w2 = Administration.UserEditor;
                var w3 = s.LookupEditor;

                Q.initFormType(CMSReportForm, [
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
