namespace AdvanceCRM.Masters {
    export interface CityForm {
        City: Serenity.StringEditor;
        StateId: Serenity.LookupEditor;
    }

    export class CityForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.City';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CityForm.init)  {
                CityForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(CityForm, [
                    'City', w0,
                    'StateId', w1
                ]);
            }
        }
    }
}
