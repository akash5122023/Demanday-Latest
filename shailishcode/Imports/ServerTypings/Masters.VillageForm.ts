namespace AdvanceCRM.Masters {
    export interface VillageForm {
        Village: Serenity.StringEditor;
        PIN: Serenity.StringEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
        TehsilId: Serenity.LookupEditor;
    }

    export class VillageForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Village';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!VillageForm.init)  {
                VillageForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(VillageForm, [
                    'Village', w0,
                    'PIN', w0,
                    'StateId', w1,
                    'CityId', w1,
                    'TehsilId', w1
                ]);
            }
        }
    }
}
