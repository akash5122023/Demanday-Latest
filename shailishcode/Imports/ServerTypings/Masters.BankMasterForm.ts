namespace AdvanceCRM.Masters {
    export interface BankMasterForm {
        BankName: Serenity.StringEditor;
        AccountNumber: Serenity.StringEditor;
        IFSC: Serenity.StringEditor;
        Type: Serenity.StringEditor;
        Branch: Serenity.StringEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
    }

    export class BankMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.BankMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BankMasterForm.init)  {
                BankMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;

                Q.initFormType(BankMasterForm, [
                    'BankName', w0,
                    'AccountNumber', w0,
                    'IFSC', w0,
                    'Type', w0,
                    'Branch', w0,
                    'AdditionalInfo', w1
                ]);
            }
        }
    }
}
