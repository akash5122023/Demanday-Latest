namespace AdvanceCRM.Toolkit {
    export interface DemandayCompetitorExcelImportForm {
        CampaignId: Serenity.LookupEditor;
        FileName: Serenity.ImageUploadEditor;
    }

    export class DemandayCompetitorExcelImportForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.DemandayCompetitorExcelImport';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayCompetitorExcelImportForm.init)  {
                DemandayCompetitorExcelImportForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.ImageUploadEditor;

                Q.initFormType(DemandayCompetitorExcelImportForm, [
                    'CampaignId', w0,
                    'FileName', w1
                ]);
            }
        }
    }
}
