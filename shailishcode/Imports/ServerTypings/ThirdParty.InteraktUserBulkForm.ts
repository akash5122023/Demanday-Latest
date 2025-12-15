namespace AdvanceCRM.ThirdParty {
    export interface InteraktUserBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class InteraktUserBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.InteraktUserBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InteraktUserBulkForm.init)  {
                InteraktUserBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(InteraktUserBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
