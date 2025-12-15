namespace AdvanceCRM.ThirdParty {
    export interface RawTelecallImportForm {
        FileName: Serenity.ImageUploadEditor;
        UIds: Serenity.LookupEditor;
    }

    export class RawTelecallImportForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.RawTelecallImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!RawTelecallImportForm.init)  {
                RawTelecallImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(RawTelecallImportForm, [
                    'FileName', w0,
                    'UIds', w1
                ]);
            }
        }
    }
}
