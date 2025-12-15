namespace AdvanceCRM.BizMail {
    export interface BmCampaignForm {
        CampaignId: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Status: Serenity.StringEditor;
    }

    export class BmCampaignForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BmCampaign';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BmCampaignForm.init)  {
                BmCampaignForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(BmCampaignForm, [
                    'CampaignId', w0,
                    'Name', w0,
                    'Status', w0
                ]);
            }
        }
    }
}
