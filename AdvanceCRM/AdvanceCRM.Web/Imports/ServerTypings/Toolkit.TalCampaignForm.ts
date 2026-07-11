namespace AdvanceCRM.Toolkit {
    export interface TalCampaignForm {
        SrNo: Serenity.IntegerEditor;
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        CompanyName: Serenity.StringEditor;
        Domain: Serenity.StringEditor;
        Cpc: Serenity.StringEditor;
        AgentsName: Serenity.LookupEditor;
        Reason: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
    }

    export class TalCampaignForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.TalCampaign';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TalCampaignForm.init)  {
                TalCampaignForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(TalCampaignForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w1,
                    'CompanyName', w2,
                    'Domain', w2,
                    'Cpc', w2,
                    'AgentsName', w1,
                    'Reason', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
