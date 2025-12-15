namespace AdvanceCRM.Common {
    export interface IntractWaForm {
        Number: Serenity.StringEditor;
        Template: Serenity.LookupEditor;
        Variable: Serenity.StringEditor;
        Image: Serenity.ImageUploadEditor;
    }

    export class IntractWaForm extends Serenity.PrefixedContext {
        static formKey = 'Common.IntractWa';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IntractWaForm.init)  {
                IntractWaForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.ImageUploadEditor;

                Q.initFormType(IntractWaForm, [
                    'Number', w0,
                    'Template', w1,
                    'Variable', w0,
                    'Image', w2
                ]);
            }
        }
    }
}
