namespace AdvanceCRM.Toolkit {
    export interface OpenCampaignForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        Domain: Serenity.StringEditor;
        TimeStamp: Serenity.DateTimeEditor;
        OwnerId: Administration.UserEditor;
    }

    export class OpenCampaignForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.OpenCampaign';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!OpenCampaignForm.init)  {
                OpenCampaignForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(OpenCampaignForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'Domain', w1,
                    'TimeStamp', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
