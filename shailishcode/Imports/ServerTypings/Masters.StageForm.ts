namespace AdvanceCRM.Masters {
    export interface StageForm {
        Stage: Serenity.StringEditor;
        Type: Serenity.EnumEditor;
    }

    export class StageForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Stage';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!StageForm.init)  {
                StageForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;

                Q.initFormType(StageForm, [
                    'Stage', w0,
                    'Type', w1
                ]);
            }
        }
    }
}
