namespace AdvanceCRM.Masters {
    export interface CategoryForm {
        Category: Serenity.StringEditor;
        Type: Serenity.IntegerEditor;
    }

    export class CategoryForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Category';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CategoryForm.init)  {
                CategoryForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;

                Q.initFormType(CategoryForm, [
                    'Category', w0,
                    'Type', w1
                ]);
            }
        }
    }
}
