namespace AdvanceCRM.DNCContact {
    export interface DncContactsForm {
        SrNo: Serenity.IntegerEditor;
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        DncStatus: Serenity.StringEditor;
        Number: Serenity.StringEditor;
    }

    export class DncContactsForm extends Serenity.PrefixedContext {
        static formKey = 'DNCContact.DncContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DncContactsForm.init)  {
                DncContactsForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;

                Q.initFormType(DncContactsForm, [
                    'SrNo', w0,
                    'MasterAccountId', w1,
                    'CampaignId', w1,
                    'FirstName', w2,
                    'LastName', w2,
                    'Email', w2,
                    'DncStatus', w2,
                    'Number', w2
                ]);
            }
        }
    }
}
