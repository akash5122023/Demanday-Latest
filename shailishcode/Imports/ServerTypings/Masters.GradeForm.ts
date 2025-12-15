namespace AdvanceCRM.Masters {
    export interface GradeForm {
        Grade: Serenity.StringEditor;
    }

    export class GradeForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Grade';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!GradeForm.init)  {
                GradeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(GradeForm, [
                    'Grade', w0
                ]);
            }
        }
    }
}
