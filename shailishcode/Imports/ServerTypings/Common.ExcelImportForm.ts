namespace AdvanceCRM.Common {
    export interface ExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class ExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Common.ExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ExcelImportForm.init)  {
                ExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(ExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
