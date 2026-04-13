namespace AdvanceCRM.Masters {
    export interface DemandayTeleMarketingEnquiryCampaignQuestionsForm {
        QuestionText: Serenity.StringEditor;
        CampaignId: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
    }

    export class DemandayTeleMarketingEnquiryCampaignQuestionsForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayTeleMarketingEnquiryCampaignQuestions';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingEnquiryCampaignQuestionsForm.init)  {
                DemandayTeleMarketingEnquiryCampaignQuestionsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(DemandayTeleMarketingEnquiryCampaignQuestionsForm, [
                    'QuestionText', w0,
                    'CampaignId', w1,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
