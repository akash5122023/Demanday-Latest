namespace AdvanceCRM.ThirdParty {
    export interface KnowlarityBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class KnowlarityBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.KnowlarityBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!KnowlarityBulkForm.init)  {
                KnowlarityBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(KnowlarityBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
