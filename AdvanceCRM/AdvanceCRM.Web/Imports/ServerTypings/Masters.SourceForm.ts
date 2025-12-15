namespace AdvanceCRM.Masters {
    export interface SourceForm {
        Source: Serenity.StringEditor;
    }

    export class SourceForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Source';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SourceForm.init)  {
                SourceForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(SourceForm, [
                    'Source', w0
                ]);
            }
        }
    }
}
