namespace AdvanceCRM.Toolkit {
    export interface ClientSupressionForm {
        SrNo: Serenity.IntegerEditor;
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
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = s.DateTimeEditor;
                var w4 = Administration.UserEditor;

                Q.initFormType(ClientSupressionForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w1,
                    'CompanyName', w2,
                    'FirstName', w2,
                    'LastName', w2,
                    'Email', w2,
                    'Domain', w2,
                    'Date', w3,
                    'OwnerId', w4
                ]);
            }
        }
    }
}
