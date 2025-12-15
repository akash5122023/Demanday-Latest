namespace AdvanceCRM.Administration {
    export interface TabGenderForm {
        Gender: Serenity.StringEditor;
    }

    export class TabGenderForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.TabGender';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TabGenderForm.init)  {
                TabGenderForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(TabGenderForm, [
                    'Gender', w0
                ]);
            }
        }
    }
}
