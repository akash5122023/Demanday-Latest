namespace AdvanceCRM.Enquiry {
    export interface EnquiryExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
        UIds: Serenity.LookupEditor;
    }

    export class EnquiryExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Enquiry.EnquiryExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryExcelImportForm.init)  {
                EnquiryExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(EnquiryExcelImportForm, [
                    'FileName', w0,
                    'UIds', w1
                ]);
            }
        }
    }
}
