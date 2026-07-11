namespace AdvanceCRM.Toolkit {
    export interface MasterSupressionExcelImportForm {
        MasterAccountId: Serenity.LookupEditor;
        FileName: Serenity.ImageUploadEditor;
    }

    export class MasterSupressionExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.MasterSupressionExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MasterSupressionExcelImportForm.init)  {
                MasterSupressionExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.ImageUploadEditor;

                Q.initFormType(MasterSupressionExcelImportForm, [
                    'MasterAccountId', w0,
                    'FileName', w1
                ]);
            }
        }
    }
}
