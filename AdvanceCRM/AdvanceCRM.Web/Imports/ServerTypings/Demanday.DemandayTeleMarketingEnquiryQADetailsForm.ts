namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingEnquiryQADetailsForm {
        CampaignId: Serenity.StringEditor;
        QuestionId: Serenity.LookupEditor;
        AnswerId: Serenity.LookupEditor;
        AnswerText: Serenity.TextAreaEditor;
    }

    export class DemandayTeleMarketingEnquiryQADetailsForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayTeleMarketingEnquiryQADetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingEnquiryQADetailsForm.init)  {
                DemandayTeleMarketingEnquiryQADetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.TextAreaEditor;

                Q.initFormType(DemandayTeleMarketingEnquiryQADetailsForm, [
                    'CampaignId', w0,
                    'QuestionId', w1,
                    'AnswerId', w1,
                    'AnswerText', w2
                ]);
            }
        }
    }
}
