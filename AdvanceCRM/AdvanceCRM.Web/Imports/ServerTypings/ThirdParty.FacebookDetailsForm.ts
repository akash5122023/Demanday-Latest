namespace AdvanceCRM.ThirdParty {
    export interface FacebookDetailsForm {
        LeadId: Serenity.StringEditor;
        Campaignid: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        CompaignName: Serenity.StringEditor;
        AdSetName: Serenity.StringEditor;
        CreatedTime: Serenity.DateTimeEditor;
        Company: Serenity.StringEditor;
        AdId: Serenity.StringEditor;
        AdName: Serenity.StringEditor;
        AdSetId: Serenity.StringEditor;
        AdditionalDetails: Serenity.StringEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class FacebookDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.FacebookDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!FacebookDetailsForm.init)  {
                FacebookDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(FacebookDetailsForm, [
                    'LeadId', w0,
                    'Campaignid', w0,
                    'Name', w0,
                    'Phone', w0,
                    'Address', w0,
                    'Email', w0,
                    'CompaignName', w0,
                    'AdSetName', w0,
                    'CreatedTime', w1,
                    'Company', w0,
                    'AdId', w0,
                    'AdName', w0,
                    'AdSetId', w0,
                    'AdditionalDetails', w0,
                    'Feedback', w2,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
