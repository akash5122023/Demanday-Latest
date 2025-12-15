namespace AdvanceCRM.Accounting {
    export interface ExpenseManagementForm {
        Date: Serenity.DateEditor;
        RepresentativeId: Administration.UserEditor;
        HeadId: Serenity.LookupEditor;
        Amount: Serenity.DecimalEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
    }

    export class ExpenseManagementForm extends Serenity.PrefixedContext {
        static formKey = 'Accounting.ExpenseManagement';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ExpenseManagementForm.init)  {
                ExpenseManagementForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = Administration.UserEditor;
                var w2 = s.LookupEditor;
                var w3 = s.DecimalEditor;
                var w4 = s.MultipleImageUploadEditor;
                var w5 = s.TextAreaEditor;

                Q.initFormType(ExpenseManagementForm, [
                    'Date', w0,
                    'RepresentativeId', w1,
                    'HeadId', w2,
                    'Amount', w3,
                    'Attachment', w4,
                    'AdditionalInfo', w5
                ]);
            }
        }
    }
}
