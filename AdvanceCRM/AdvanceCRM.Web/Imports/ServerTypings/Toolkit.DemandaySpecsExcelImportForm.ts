namespace AdvanceCRM.Toolkit {
    export interface DemandaySpecsExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class DemandaySpecsExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.DemandaySpecsExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandaySpecsExcelImportForm.init)  {
                DemandaySpecsExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(DemandaySpecsExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
