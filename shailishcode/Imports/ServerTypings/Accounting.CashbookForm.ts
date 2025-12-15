namespace AdvanceCRM.Accounting {
    export interface CashbookForm {
        Date: Serenity.DateEditor;
        RepresentativeId: Administration.UserEditor;
        Type: Serenity.EnumEditor;
        Head: Serenity.LookupEditor;
        ProjectId: Serenity.LookupEditor;
        ContactsId: Serenity.LookupEditor;
        EmployeeId: Serenity.LookupEditor;
        InvoiceNo: Serenity.StringEditor;
        Purpose: Serenity.StringEditor;
        IsCashIn: BooleanSwitchEditor;
        ProjectAmtIn: Serenity.DecimalEditor;
        CashIn: Serenity.DecimalEditor;
        CashOut: Serenity.DecimalEditor;
        Narration: Serenity.TextAreaEditor;
        BankId: Serenity.LookupEditor;
    }

    export class CashbookForm extends Serenity.PrefixedContext {
        static formKey = 'Accounting.Cashbook';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CashbookForm.init)  {
                CashbookForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = Administration.UserEditor;
                var w2 = s.EnumEditor;
                var w3 = s.LookupEditor;
                var w4 = s.StringEditor;
                var w5 = BooleanSwitchEditor;
                var w6 = s.DecimalEditor;
                var w7 = s.TextAreaEditor;

                Q.initFormType(CashbookForm, [
                    'Date', w0,
                    'RepresentativeId', w1,
                    'Type', w2,
                    'Head', w3,
                    'ProjectId', w3,
                    'ContactsId', w3,
                    'EmployeeId', w3,
                    'InvoiceNo', w4,
                    'Purpose', w4,
                    'IsCashIn', w5,
                    'ProjectAmtIn', w6,
                    'CashIn', w6,
                    'CashOut', w6,
                    'Narration', w7,
                    'BankId', w3
                ]);
            }
        }
    }
}
