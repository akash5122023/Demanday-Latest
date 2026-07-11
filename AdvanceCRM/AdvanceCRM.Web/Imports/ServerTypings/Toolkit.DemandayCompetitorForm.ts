namespace AdvanceCRM.Toolkit {
    export interface DemandayCompetitorForm {
        SrNo: Serenity.IntegerEditor;
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
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(DemandayCompetitorForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w1,
                    'CompanyName', w2,
                    'Domain', w2,
                    'Email', w2,
                    'Cpc', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
