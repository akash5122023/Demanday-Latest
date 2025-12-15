namespace AdvanceCRM.Masters {
    export interface AreaForm {
        Area: Serenity.StringEditor;
    }

    export class AreaForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Area';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AreaForm.init)  {
                AreaForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(AreaForm, [
                    'Area', w0
                ]);
            }
        }
    }
}
