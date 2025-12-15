namespace AdvanceCRM.BizMail {
    export interface BizMailQuotationForm {
        Rule: Serenity.EnumEditor;
        QuotationStatus: Serenity.EnumEditor;
        ClosingType: Serenity.EnumEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        Type: Serenity.EnumEditor;
        BmListId: Serenity.LookupEditor;
        Status: BSSwitchEditor;
        CompanyId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class BizMailQuotationForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BizMailQuotation';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BizMailQuotationForm.init)  {
                BizMailQuotationForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;
                var w2 = BSSwitchEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(BizMailQuotationForm, [
                    'Rule', w0,
                    'QuotationStatus', w0,
                    'ClosingType', w0,
                    'SourceId', w1,
                    'StageId', w1,
                    'Type', w0,
                    'BmListId', w1,
                    'Status', w2,
                    'CompanyId', w1,
                    'Description', w3
                ]);
            }
        }
    }
}
