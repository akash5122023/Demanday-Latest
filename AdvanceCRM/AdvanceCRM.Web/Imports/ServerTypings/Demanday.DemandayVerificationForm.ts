namespace AdvanceCRM.Demanday {
    export interface DemandayVerificationForm {
        AgentName: Serenity.StringEditor;
        CdqaComments: _Ext.HardCodedLookupEditor;
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
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
                var w0 = s.StringEditor;
                var w1 = _Ext.HardCodedLookupEditor;
                var w2 = s.LookupEditor;
                var w3 = s.EmailEditor;
                var w4 = s.DateTimeEditor;

                Q.initFormType(DemandayVerificationForm, [
                    'AgentName', w0,
                    'CdqaComments', w1,
                    'MasterAccountId', w2,
                    'CampaignId', w2,
                    'CompanyName', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Title', w0,
                    'Email', w3,
                    'Date', w4,
                    'WorkPhone', w0,
                    'Alternate01', w0,
                    'Alternate02', w0,
                    'ProfileLink', w0,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
