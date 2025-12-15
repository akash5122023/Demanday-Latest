namespace AdvanceCRM.BizMail {
    export interface BizMailWooComForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailWooComForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailWooCom';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailWooComForm.init)  {
                BizMailWooComForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailWooComForm, [
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
