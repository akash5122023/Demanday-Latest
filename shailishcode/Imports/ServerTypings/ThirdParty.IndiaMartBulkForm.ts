namespace AdvanceCRM.ThirdParty {
    export interface IndiaMartBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class IndiaMartBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.IndiaMartBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IndiaMartBulkForm.init)  {
                IndiaMartBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(IndiaMartBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
