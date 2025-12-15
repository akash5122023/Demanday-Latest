namespace AdvanceCRM.Contacts {
    export interface SubContactExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class SubContactExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Contacts.SubContactExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SubContactExcelImportForm.init)  {
                SubContactExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(SubContactExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
