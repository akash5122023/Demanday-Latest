namespace AdvanceCRM.BizMail {
    export interface CampaignBmForm {
        Campaignuid: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Type: Serenity.EnumEditor;
        FromName: Serenity.StringEditor;
        FromEmail: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        ReplyTo: Serenity.StringEditor;
        BmListId: Serenity.LookupEditor;
        ListId: Serenity.StringEditor;
        SendAt: Serenity.DateEditor;
        BmTemplateId: Serenity.LookupEditor;
        InlineCss: Serenity.EnumEditor;
        AutoPlaneTest: Serenity.EnumEditor;
    }

    export class CampaignBmForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.CampaignBm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CampaignBmForm.init)  {
                CampaignBmForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;
                var w2 = s.LookupEditor;
                var w3 = s.DateEditor;

                Q.initFormType(CampaignBmForm, [
                    'Campaignuid', w0,
                    'Name', w0,
                    'Type', w1,
                    'FromName', w0,
                    'FromEmail', w0,
                    'Subject', w0,
                    'ReplyTo', w0,
                    'BmListId', w2,
                    'ListId', w0,
                    'SendAt', w3,
                    'BmTemplateId', w2,
                    'InlineCss', w1,
                    'AutoPlaneTest', w1
                ]);
            }
        }
    }
}
