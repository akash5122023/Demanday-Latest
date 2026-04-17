namespace AdvanceCRM.Toolkit {
    export interface TalCampaignExcelImportForm {
        FileName: Serenity.ImageUploadEditor;
    }

    export class TalCampaignExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.TalCampaignExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TalCampaignExcelImportForm.init)  {
                TalCampaignExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.ImageUploadEditor;

                Q.initFormType(TalCampaignExcelImportForm, [
                    'FileName', w0
                ]);
            }
        }
    }
}
