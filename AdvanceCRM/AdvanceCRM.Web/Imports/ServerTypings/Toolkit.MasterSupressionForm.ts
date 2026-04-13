namespace AdvanceCRM.Toolkit {
    export interface MasterSupressionForm {
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

    export class MasterSupressionForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.MasterSupression';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MasterSupressionForm.init)  {
                MasterSupressionForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(MasterSupressionForm, [
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
