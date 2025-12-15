namespace AdvanceCRM.BizMail {
    export interface BizMailVisitForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailVisitForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailVisit';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailVisitForm.init)  {
                BizMailVisitForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailVisitForm, [
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
