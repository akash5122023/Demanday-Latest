namespace AdvanceCRM.Masters {
    export interface DemandayMasterAccountForm {
        AccountNumber: Serenity.StringEditor;
    }

    export class DemandayMasterAccountForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayMasterAccount';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayMasterAccountForm.init)  {
                DemandayMasterAccountForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandayMasterAccountForm, [
                    'AccountNumber', w0
                ]);
            }
        }
    }
}
