namespace AdvanceCRM.VerifySheet {
    export interface TMEnquiryForm {
        MasterAccountId: Serenity.IntegerEditor;
        CampaignId: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Timestamp: Serenity.DateEditor;
        CreatedOn: Serenity.DateEditor;
        CreatedBy: Serenity.StringEditor;
        UpdatedOn: Serenity.DateEditor;
        UpdatedBy: Serenity.StringEditor;
    }

    export class TMEnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'VerifySheet.TMEnquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TMEnquiryForm.init)  {
                TMEnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateEditor;

                Q.initFormType(TMEnquiryForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w1,
                    'FirstName', w1,
                    'LastName', w1,
                    'Email', w1,
                    'CompanyName', w1,
                    'Timestamp', w2,
                    'CreatedOn', w2,
                    'CreatedBy', w1,
                    'UpdatedOn', w2,
                    'UpdatedBy', w1
                ]);
            }
        }
    }
}
