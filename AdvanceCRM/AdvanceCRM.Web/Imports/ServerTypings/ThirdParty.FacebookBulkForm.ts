namespace AdvanceCRM.ThirdParty {
    export interface FacebookBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class FacebookBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.FacebookBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!FacebookBulkForm.init)  {
                FacebookBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(FacebookBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
