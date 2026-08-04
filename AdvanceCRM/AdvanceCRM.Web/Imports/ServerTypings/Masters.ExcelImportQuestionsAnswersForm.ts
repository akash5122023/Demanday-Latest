namespace AdvanceCRM.Masters {
    export interface ExcelImportQuestionsAnswersForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class ExcelImportQuestionsAnswersForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ExcelImportQuestionsAnswers';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ExcelImportQuestionsAnswersForm.init)  {
                ExcelImportQuestionsAnswersForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(ExcelImportQuestionsAnswersForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
