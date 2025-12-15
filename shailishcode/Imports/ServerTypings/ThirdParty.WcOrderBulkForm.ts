namespace AdvanceCRM.ThirdParty {
    export interface WcOrderBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class WcOrderBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WcOrderBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WcOrderBulkForm.init)  {
                WcOrderBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(WcOrderBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
