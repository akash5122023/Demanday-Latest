namespace AdvanceCRM.BizMail {
    export interface BizMailWebForm {
        Rule: Serenity.EnumEditor;
        BmListId: Serenity.IntegerEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.IntegerEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailWebForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailWeb';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailWebForm.init)  {
                BizMailWebForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.IntegerEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailWebForm, [
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
