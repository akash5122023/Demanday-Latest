namespace AdvanceCRM.Masters {
    export interface TehsilExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class TehsilExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.TehsilExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TehsilExcelImportForm.init)  {
                TehsilExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(TehsilExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
