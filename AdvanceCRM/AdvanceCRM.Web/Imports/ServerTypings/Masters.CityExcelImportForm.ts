namespace AdvanceCRM.Masters {
    export interface CityExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class CityExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.CityExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CityExcelImportForm.init)  {
                CityExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(CityExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
