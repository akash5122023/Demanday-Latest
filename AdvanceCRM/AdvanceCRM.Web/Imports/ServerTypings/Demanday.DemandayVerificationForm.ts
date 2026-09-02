namespace AdvanceCRM.Demanday {
    export interface DemandayVerificationForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        AgentName: Serenity.StringEditor;
        CdqaComments: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        Date: Serenity.DateTimeEditor;
        WorkPhone: Serenity.StringEditor;
        Alternate01: Serenity.StringEditor;
        Alternate02: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        OwnerId: Serenity.LookupEditor;
    }

    export class DemandayVerificationForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayVerification';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayVerificationForm.init)  {
                DemandayVerificationForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.EmailEditor;
                var w3 = s.DateTimeEditor;

                Q.initFormType(DemandayVerificationForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'AgentName', w1,
                    'CdqaComments', w1,
                    'CompanyName', w1,
                    'FirstName', w1,
                    'LastName', w1,
                    'Title', w1,
                    'Email', w2,
                    'Date', w3,
                    'WorkPhone', w1,
                    'Alternate01', w1,
                    'Alternate02', w1,
                    'ProfileLink', w1,
                    'OwnerId', w0
                ]);
            }
        }
    }
}
