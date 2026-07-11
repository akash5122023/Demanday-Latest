namespace AdvanceCRM.Toolkit {
    export interface ClientSupressionExcelImportForm {
        CampaignId: Serenity.LookupEditor;
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
                var w0 = s.LookupEditor;
                var w1 = s.ImageUploadEditor;

                Q.initFormType(ClientSupressionExcelImportForm, [
                    'CampaignId', w0,
                    'FileName', w1
                ]);
            }
        }
    }
}
