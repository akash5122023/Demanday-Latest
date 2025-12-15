namespace AdvanceCRM.Demanday {
    export interface DemandayVerificationForm {
        AgentName: Serenity.StringEditor;
        CdqaComments: Serenity.TextAreaEditor;
        CompanyName: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
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
                var w1 = s.TextAreaEditor;
                var w2 = s.EmailEditor;
                var w3 = s.LookupEditor;

                Q.initFormType(DemandayVerificationForm, [
                    'AgentName', w0,
                    'CdqaComments', w1,
                    'CompanyName', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Title', w0,
                    'Email', w2,
                    'WorkPhone', w0,
                    'Alternate01', w0,
                    'Alternate02', w0,
                    'ProfileLink', w0,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
