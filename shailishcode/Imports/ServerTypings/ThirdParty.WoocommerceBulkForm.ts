namespace AdvanceCRM.ThirdParty {
    export interface WoocommerceBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class WoocommerceBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WoocommerceBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WoocommerceBulkForm.init)  {
                WoocommerceBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(WoocommerceBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
