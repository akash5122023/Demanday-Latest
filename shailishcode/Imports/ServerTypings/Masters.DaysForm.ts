namespace AdvanceCRM.Masters {
    export interface DaysForm {
        Title: Serenity.StringEditor;
        Heading: Serenity.StringEditor;
        Description: Serenity.TextAreaEditor;
        FileAttachments: Serenity.ImageUploadEditor;
    }

    export class DaysForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Days';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DaysForm.init)  {
                DaysForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.ImageUploadEditor;

                Q.initFormType(DaysForm, [
                    'Title', w0,
                    'Heading', w0,
                    'Description', w1,
                    'FileAttachments', w2
                ]);
            }
        }
    }
}
