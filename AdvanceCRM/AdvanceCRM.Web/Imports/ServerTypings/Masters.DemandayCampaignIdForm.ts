namespace AdvanceCRM.Masters {
    export interface DemandayCampaignIdForm {
        CampaignId: Serenity.StringEditor;
        DemandayMasterAccountId: Serenity.LookupEditor;
    }

    export class DemandayCampaignIdForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayCampaignId';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayCampaignIdForm.init)  {
                DemandayCampaignIdForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;

                Q.initFormType(DemandayCampaignIdForm, [
                    'CampaignId', w0,
                    'DemandayMasterAccountId', w1
                ]);
            }
        }
    }
}
