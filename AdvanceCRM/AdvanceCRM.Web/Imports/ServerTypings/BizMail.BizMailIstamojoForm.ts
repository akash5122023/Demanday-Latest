namespace AdvanceCRM.BizMail {
    export interface BizMailIstamojoForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailIstamojoForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailIstamojo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailIstamojoForm.init)  {
                BizMailIstamojoForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailIstamojoForm, [
                    'Rule', w0,
                    'BmListId', w1,
                    'Status', w2,
                    'CompanyId', w1,
                    'Description', w3
                ]);
            }
        }
    }
}
