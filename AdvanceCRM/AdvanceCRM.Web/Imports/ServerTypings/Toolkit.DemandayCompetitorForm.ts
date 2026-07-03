namespace AdvanceCRM.Toolkit {
    export interface DemandayCompetitorForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        CompanyName: Serenity.StringEditor;
        Domain: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Cpc: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
    }

    export class DemandayCompetitorForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.DemandayCompetitor';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayCompetitorForm.init)  {
                DemandayCompetitorForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(DemandayCompetitorForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'CompanyName', w1,
                    'Domain', w1,
                    'Email', w1,
                    'Cpc', w1,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
