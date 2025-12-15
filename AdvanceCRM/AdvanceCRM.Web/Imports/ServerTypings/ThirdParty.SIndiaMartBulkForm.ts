namespace AdvanceCRM.ThirdParty {
    export interface SIndiaMartBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class SIndiaMartBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.SIndiaMartBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SIndiaMartBulkForm.init)  {
                SIndiaMartBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(SIndiaMartBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
