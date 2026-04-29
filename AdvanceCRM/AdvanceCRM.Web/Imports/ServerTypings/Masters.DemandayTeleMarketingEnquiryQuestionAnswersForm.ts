namespace AdvanceCRM.Masters {
    export interface DemandayTeleMarketingEnquiryQuestionAnswersForm {
        CampaignId: Serenity.LookupEditor;
        QuestionId: Serenity.LookupEditor;
        AnswerText: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
    }

    export class DemandayTeleMarketingEnquiryQuestionAnswersForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayTeleMarketingEnquiryQuestionAnswers';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingEnquiryQuestionAnswersForm.init)  {
                DemandayTeleMarketingEnquiryQuestionAnswersForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(DemandayTeleMarketingEnquiryQuestionAnswersForm, [
                    'CampaignId', w0,
                    'QuestionId', w0,
                    'AnswerText', w1,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
