namespace AdvanceCRM.ThirdParty {
    export interface WatiContactsBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class WatiContactsBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WatiContactsBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WatiContactsBulkForm.init)  {
                WatiContactsBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(WatiContactsBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
