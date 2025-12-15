namespace AdvanceCRM.ThirdParty {
    export interface SulekhaBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class SulekhaBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.SulekhaBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SulekhaBulkForm.init)  {
                SulekhaBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(SulekhaBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
