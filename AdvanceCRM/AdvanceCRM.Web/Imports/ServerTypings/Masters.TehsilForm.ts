namespace AdvanceCRM.Masters {
    export interface TehsilForm {
        Tehsil: Serenity.StringEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
    }

    export class TehsilForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Tehsil';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TehsilForm.init)  {
                TehsilForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(TehsilForm, [
                    'Tehsil', w0,
                    'StateId', w1,
                    'CityId', w1
                ]);
            }
        }
    }
}
