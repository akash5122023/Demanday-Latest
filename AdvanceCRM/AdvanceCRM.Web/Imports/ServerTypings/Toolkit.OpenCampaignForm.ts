namespace AdvanceCRM.Toolkit {
    export interface OpenCampaignForm {
        SrNo: Serenity.IntegerEditor;
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
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = s.DateTimeEditor;
                var w4 = Administration.UserEditor;

                Q.initFormType(OpenCampaignForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w1,
                    'Domain', w2,
                    'TimeStamp', w3,
                    'OwnerId', w4
                ]);
            }
        }
    }
}
