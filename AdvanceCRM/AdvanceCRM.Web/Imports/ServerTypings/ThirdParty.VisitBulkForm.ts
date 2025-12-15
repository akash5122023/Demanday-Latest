namespace AdvanceCRM.ThirdParty {
    export interface VisitBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class VisitBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.VisitBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!VisitBulkForm.init)  {
                VisitBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(VisitBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
