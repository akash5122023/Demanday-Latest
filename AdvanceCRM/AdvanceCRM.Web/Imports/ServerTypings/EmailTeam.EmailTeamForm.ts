namespace AdvanceCRM.EmailTeam {
    export interface EmailTeamForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Status: Serenity.EnumEditor;
        OwnerId: Administration.UserEditor;
    }

    export class EmailTeamForm extends Serenity.PrefixedContext {
        static formKey = 'EmailTeam.EmailTeam';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EmailTeamForm.init)  {
                EmailTeamForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.EnumEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(EmailTeamForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'FirstName', w1,
                    'LastName', w1,
                    'Email', w1,
                    'Status', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
