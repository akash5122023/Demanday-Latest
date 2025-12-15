namespace AdvanceCRM.Masters {
    export interface VillageExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class VillageExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.VillageExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!VillageExcelImportForm.init)  {
                VillageExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(VillageExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
