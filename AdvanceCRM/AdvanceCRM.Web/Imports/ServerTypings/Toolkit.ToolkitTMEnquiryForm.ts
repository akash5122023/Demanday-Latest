namespace AdvanceCRM.Toolkit {
    export interface ToolkitTMEnquiryForm {
        SrNo: Serenity.IntegerEditor;
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Timestamp: Serenity.DateEditor;
        DemandayTeleMarketingEnquiryId: Serenity.LookupEditor;
    }

    export class ToolkitTMEnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.ToolkitTMEnquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ToolkitTMEnquiryForm.init)  {
                ToolkitTMEnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = s.DateEditor;

                Q.initFormType(ToolkitTMEnquiryForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w2,
                    'FirstName', w2,
                    'LastName', w2,
                    'Email', w2,
                    'CompanyName', w2,
                    'Timestamp', w3,
                    'DemandayTeleMarketingEnquiryId', w1
                ]);
            }
        }
    }
}
