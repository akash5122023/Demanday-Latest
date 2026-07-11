namespace AdvanceCRM.DNCContact {
    export interface DncContactsForm {
        MasterAccountId: Serenity.IntegerEditor;
        CampaignId: Serenity.IntegerEditor;
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
                var w1 = s.StringEditor;

                Q.initFormType(DncContactsForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'FirstName', w1,
                    'LastName', w1,
                    'Email', w1,
                    'DncStatus', w1,
                    'Number', w1
                ]);
            }
        }
    }
}
