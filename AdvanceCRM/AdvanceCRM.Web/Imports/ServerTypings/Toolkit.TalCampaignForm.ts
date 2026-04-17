namespace AdvanceCRM.Toolkit {
    export interface TalCampaignForm {
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
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(TalCampaignForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'CompanyName', w1,
                    'Domain', w1,
                    'Cpc', w1,
                    'AgentsName', w0,
                    'Reason', w1,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
