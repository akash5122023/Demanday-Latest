namespace AdvanceCRM.Toolkit {
    export interface ClientSupressionExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class ClientSupressionExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.ClientSupressionExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ClientSupressionExcelImportForm.init)  {
                ClientSupressionExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(ClientSupressionExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
