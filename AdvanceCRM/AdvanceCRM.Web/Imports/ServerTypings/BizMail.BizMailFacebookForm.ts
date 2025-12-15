namespace AdvanceCRM.BizMail {
    export interface BizMailFacebookForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailFacebookForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailFacebook';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailFacebookForm.init)  {
                BizMailFacebookForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailFacebookForm, [
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
