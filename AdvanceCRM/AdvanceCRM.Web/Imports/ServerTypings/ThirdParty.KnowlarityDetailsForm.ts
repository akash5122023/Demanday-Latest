namespace AdvanceCRM.ThirdParty {
    export interface KnowlarityDetailsForm {
        Name: Serenity.StringEditor;
        CustomerNumber: Serenity.StringEditor;
        EmployeeName: Serenity.StringEditor;
        Cmiuid: Serenity.StringEditor;
        BilledSec: Serenity.StringEditor;
        Rate: Serenity.StringEditor;
        Record: Serenity.StringEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Type: Serenity.StringEditor;
        CompanyType: Serenity.IntegerEditor;
        Email: Serenity.StringEditor;
        EmployeeNumber: Serenity.StringEditor;
        Duration: Serenity.StringEditor;
        Recording: Serenity.StringEditor;
        DateTime: Serenity.DateTimeEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class KnowlarityDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.KnowlarityDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!KnowlarityDetailsForm.init)  {
                KnowlarityDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(KnowlarityDetailsForm, [
                    'Name', w0,
                    'CustomerNumber', w0,
                    'EmployeeName', w0,
                    'Cmiuid', w0,
                    'BilledSec', w0,
                    'Rate', w0,
                    'Record', w0,
                    'From', w0,
                    'To', w0,
                    'Type', w0,
                    'CompanyType', w1,
                    'Email', w0,
                    'EmployeeNumber', w0,
                    'Duration', w0,
                    'Recording', w0,
                    'DateTime', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
