namespace AdvanceCRM.Toolkit {
    export interface OpenCampaignForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        Domain: Serenity.StringEditor;
        DemandayUserId: Serenity.IntegerEditor;
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
                var w2 = s.IntegerEditor;
                var w3 = s.DateTimeEditor;
                var w4 = Administration.UserEditor;

                Q.initFormType(OpenCampaignForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'Domain', w1,
                    'DemandayUserId', w2,
                    'TimeStamp', w3,
                    'OwnerId', w4
                ]);
            }
        }
    }
}
