namespace AdvanceCRM.Reports {
    export interface AccountingReportForm {
        Type: Serenity.EnumEditor;
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        Contact: Serenity.LookupEditor;
        Head: Serenity.LookupEditor;
        Employee: Serenity.LookupEditor;
        Project: Serenity.LookupEditor;
        Bank: Serenity.LookupEditor;
        CashIn: BSSwitchEditor;
        CashOut: BSSwitchEditor;
    }

    export class AccountingReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.AccountingReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AccountingReportForm.init)  {
                AccountingReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.DateEditor;
                var w2 = s.LookupEditor;
                var w3 = BSSwitchEditor;

                Q.initFormType(AccountingReportForm, [
                    'Type', w0,
                    'DateFrom', w1,
                    'DateTo', w1,
                    'Contact', w2,
                    'Head', w2,
                    'Employee', w2,
                    'Project', w2,
                    'Bank', w2,
                    'CashIn', w3,
                    'CashOut', w3
                ]);
            }
        }
    }
}
