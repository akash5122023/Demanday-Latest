namespace AdvanceCRM.Masters {
    export interface AccountingHeadsForm {
        Head: Serenity.StringEditor;
        Type: Serenity.EnumEditor;
    }

    export class AccountingHeadsForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.AccountingHeads';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AccountingHeadsForm.init)  {
                AccountingHeadsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;

                Q.initFormType(AccountingHeadsForm, [
                    'Head', w0,
                    'Type', w1
                ]);
            }
        }
    }
}
