namespace AdvanceCRM.BizMail {
    export interface BizMailJustDialForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailJustDialForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailJustDial';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailJustDialForm.init)  {
                BizMailJustDialForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailJustDialForm, [
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
