namespace AdvanceCRM.Toolkit {
    export interface ClientSupressionForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        CompanyName: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Domain: Serenity.StringEditor;
        Date: Serenity.DateTimeEditor;
        OwnerId: Administration.UserEditor;
    }

    export class ClientSupressionForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.ClientSupression';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ClientSupressionForm.init)  {
                ClientSupressionForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(ClientSupressionForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'CompanyName', w1,
                    'FirstName', w1,
                    'LastName', w1,
                    'Email', w1,
                    'Domain', w1,
                    'Date', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
