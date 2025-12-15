namespace AdvanceCRM.Reports {
    export interface StockReportForm {
        Type: Serenity.EnumEditor;
        Branch: Serenity.LookupEditor;
        Division: Serenity.LookupEditor;
        Group: Serenity.LookupEditor;
        Product: Serenity.LookupEditor;
    }

    export class StockReportForm extends Serenity.PrefixedContext {
        static formKey = 'Reports.StockReportForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!StockReportForm.init)  {
                StockReportForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(StockReportForm, [
                    'Type', w0,
                    'Branch', w1,
                    'Division', w1,
                    'Group', w1,
                    'Product', w1
                ]);
            }
        }
    }
}
