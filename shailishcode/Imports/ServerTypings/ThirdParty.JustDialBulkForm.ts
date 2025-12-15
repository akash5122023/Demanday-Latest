namespace AdvanceCRM.ThirdParty {
    export interface JustDialBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class JustDialBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.JustDialBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!JustDialBulkForm.init)  {
                JustDialBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(JustDialBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
